import { Doctor } from './../../_models/doctor';
import { DocumentType } from './../../_models/documentType';
import { ActivatedRoute } from '@angular/router';
import { Document } from './../../_models/document';
import { environment } from './../../../environments/environment';
import { Patient } from './../../_models/patient';
import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-patient-for-registry',
  templateUrl: './patient-for-registry.component.html',
  styleUrls: ['./patient-for-registry.component.css']
})
export class PatientForRegistryComponent implements OnInit {
  public patient: Patient;
  public docTypes: DocumentType[];
  public doctors: Doctor[];
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  // hasAnotherDropZoneOver = false;
  baseUrl = environment.apiUrl;
  // response: string;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.patient = data.patient;
      this.docTypes = data.docTypes;
      this.doctors = data.doctors;
    });
    this.initUploader();
  }
  public isNewPatient(): boolean {
    if (this.patient.id) {
      return true;
    } else {
      return false;
    }
  }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
  private initUploader(): void {
    this.uploader = new FileUploader({
      url: this.baseUrl, // TODO: указать путь до контроллера по приему документов
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image', 'tiff', 'doc', 'docx', 'pdf'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Document = JSON.parse(response);
        const document = {
          id: res.id,
          docName: res.docName,
        };
      }
    };
  }

  // public fileOverAnother(e: any): void {
  //   this.hasAnotherDropZoneOver = e;
  // }
}
