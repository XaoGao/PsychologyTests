import { Document } from './../../_models/document';
import { DocumentService } from './../../_services/document.service';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { ToastrAlertService } from '../../_services/toastr-alert.service';
import { Doctor } from '../../_models/doctor';
import { DocumentType } from '../../_models/documentType';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { Patient } from '../../_models/patient';
import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { PatientService } from 'src/app/_services/patient.service';
import { saveAs} from 'file-saver';

@Component({
  selector: 'app-patient-for-registry',
  templateUrl: './patient-for-registry.component.html',
  styleUrls: ['./patient-for-registry.component.css']
})
export class PatientForRegistryComponent implements OnInit {
  public patient: Patient;
  public docTypes: DocumentType[];
  public doctors: Doctor[];
  public documents: Document[];
  public doc: Document = new Document();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  public minDate: Date = new Date(1940, 1, 1);
  constructor(private route: ActivatedRoute,
              private patientService: PatientService,
              private authService: AuthService,
              private toastrService: ToastrAlertService,
              private router: Router,
              private documentService: DocumentService) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.patient = data.patient;
      this.docTypes = data.docTypes;
      this.doctors = data.doctors;
      this.documents = data.documents;
    });
    this.isNewPatient();
    this.initUploader();
  }
  /**
   * Проверка новый пациент или нет(создать пациента или обновить сущесвтующего)
   */
  public isNewPatient(): boolean {
    if (this.patient.id) {
      return false;
    } else {
      return true;
    }
  }
  /**
   * Инициализация области загрузки документов
   */
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  private initUploader(): void {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'doctors/' + this.authService.doctorId + '/patients/' + this.patient.id + '/documents',
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

    this.uploader.onBeforeUploadItem = (file) => {
      this.uploader.setOptions({additionalParameter: {
        number: this.doc.number,
        patientId: this.patient.id,
        series: this.doc.series,
        documentTypeId: this.doc.documentTypeId}});
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Document = JSON.parse(response);
        const document = {
          id: res.id,
          docName: res.docName,
          series: res.series,
          number: res.number,
          dateUpload: res.dateUpload,
          documentTypeId: res.documentTypeId,
          documentType: res.documentType,
          patientId: res.patientId,
          patient: res.patient,
          interdepartStatusId: res.interdepartStatusId,
          interdepartRequestId: res.interdepartRequestId,
        };
        this.documents.push(document);
      }
    };
  }
  /**
   * Сохрнить данные(для нового пациента создать его, для существующего обновить данные )
   */
  public save(patientForm: NgForm): void {
    if (this.isNewPatient()) {
      this.createPatient(this.patient, patientForm);
    } else {
      this.updatePatient(this.patient);
    }
  }
  private updatePatient(patient: Patient) {
    this.patientService.updatePatient(this.authService.doctorId, patient.id, patient).subscribe((res) => {
       const newpatient = res as Patient;
       this.toastrService.success(`Данные ${newpatient.fullname} пациента успешно обновлены`);
       this.router.navigate(['/patientsforregistry']);
      }
    , err => {
      this.toastrService.error(err);
    });
  }
  private createPatient(patient: Patient, patientForm: NgForm) {
    this.patientService.createPatient(this.authService.doctorId, patient).subscribe((res) => {
      const newpatient = res as Patient;
      this.toastrService.success(`Пациент ${newpatient.fullname} успешно добавлен в систему`);
      patientForm.resetForm();
      this.router.navigate(['/patientsforregistry/', newpatient.id]);
    }, err => {
      this.toastrService.error(err);
    });
  }
  public downloadDoc(document: Document): void {
    this.documentService.downloadDocument(this.authService.doctorId, this.patient.id, document.id).subscribe((data: any) => {
      this.downloadFile(data, document.docName);
    }, err => {
      this.toastrService.error(err);
    });
  }
  private downloadFile(data: any, fileName: string) {
    const blob = new Blob([data], { type: 'application/octet-stream' });
    saveAs(blob, fileName);
  }
  public deletePatient() {
    if (confirm(`Вы уверены, что хотите удалить из системы пациента: ${this.patient.fullname}?`)) {
      this.patientService.deletePatient(this.authService.doctorId, this.patient.id).subscribe(() => {
        this.toastrService.success('Вы удалили пользователя');
        this.router.navigate(['/patientsforregistry']);
      }, err => {
        this.toastrService.error(err);
      });
    }
  }
  public haveDocuments(): boolean {
    if (this.patient.documents.length > 0) {
      return true;
    } else {
      return false;
    }
  }
  public deleteDoc(document: Document) {
    this.documentService.deleteDocument(this.authService.doctorId, this.patient.id, document.id).subscribe(() => {
      this.toastrService.success('Документ успешно удален');
      const index = this.documents.indexOf(document, 0);
      if (index >= 0) {
        this.documents.splice(index, 1);
      }
    }, err => {
      this.toastrService.error(err);
    });
  }
  public interdepartStatusName(interdepartStatusId: number): string {
    return this.documentService.interdepartStatusName(interdepartStatusId);
  }
  public interdepartRequest(documentId: number): void {
    this.documentService.interdepartReguest(documentId).subscribe(() => {
      this.toastrService.info('Запрос отпрален');
    }, err => {
      this.toastrService.error(err);
    });
  }
  public getSeriesNumber(item: string): string {
    if (item !== 'undefined') {
      return item;
    } else {
      return '-';
    }
  }
}
