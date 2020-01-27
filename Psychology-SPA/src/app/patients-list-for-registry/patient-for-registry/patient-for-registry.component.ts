import { AuthService } from './../../_services/auth.service';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Doctor } from './../../_models/doctor';
import { DocumentType } from './../../_models/documentType';
import { ActivatedRoute, Router } from '@angular/router';
import { Document } from './../../_models/document';
import { environment } from './../../../environments/environment';
import { Patient } from './../../_models/patient';
import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { PatientService } from 'src/app/_services/patient.service';

@Component({
  selector: 'app-patient-for-registry',
  templateUrl: './patient-for-registry.component.html',
  styleUrls: ['./patient-for-registry.component.css']
})
export class PatientForRegistryComponent implements OnInit {
  public patient: Patient;
  public docTypes: DocumentType[];
  public doctors: Doctor[];
  public doc: Document = new Document();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  // hasAnotherDropZoneOver = false;
  baseUrl = environment.apiUrl;
  // response: string;
  constructor(private route: ActivatedRoute,
              private patientService: PatientService,
              private authService: AuthService,
              private toastrService: ToastrAlertService,
              private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.patient = data.patient;
      this.docTypes = data.docTypes;
      this.doctors = data.doctors;
    });
    this.isNewPatient();
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
      url: this.baseUrl + 'doctors/' + this.authService.decodedToken.nameid + '/patients/' + this.patient.id + '/doc',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image', 'tiff', 'doc', 'docx', 'pdf'],
      removeAfterUpload: false,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      console.log(this.doc);
      file.withCredentials = false;
      this.uploader.setOptions({additionalParameter: {number: this.doc.number,
        patientId: this.patient.id,
        series: this.doc.series,
        documentTypeId: this.docTypes[0].id}});
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Document = JSON.parse(response);
        const document = {
          id: res.id,
          docName: res.docName,
          patientId: res.patientId,
          number: res.number,
          series: res.series
        };
      }
    };
  }
  public save(): void {
    if (this.isNewPatient()) {
      console.log('save');
      console.log(this.patient);
    } else {
      console.log('update');
      console.log(this.patient);
    }
  }
  private updatePatient(patient: Patient) {
    this.patientService.updatePatient(this.authService.decodedToken.nameid, patient.id, patient).subscribe((res) => {
       const newpatient = res as Patient;
       this.toastrService.success(`Данные ${newpatient.fullname} пациента успешно обновлены`);
       this.router.navigate(['/patientsforregistry']);
      }
    , err => {
      this.toastrService.error(err);
    });
  }
  private createPatient(patient: Patient) {
    this.patientService.createPatient(this.authService.decodedToken.nameid, patient).subscribe((res) => {
      const newpatient = res as Patient;
      this.toastrService.success(`Пациент ${newpatient.fullname} успешно добавлен в систему`);
      this.router.navigate(['/patientsforregistry']);
    }, err => {
      this.toastrService.error(err);
    });
  }
  public deletePatient() {
    console.log('delete');
    console.log(this.patient.id);
    this.router.navigate(['/patientsforregistry']);
  }
  // public fileOverAnother(e: any): void {
  //   this.hasAnotherDropZoneOver = e;
  // }
}
