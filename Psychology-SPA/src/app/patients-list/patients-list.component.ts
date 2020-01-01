import { ToastrAlertService } from './../_services/toastr-alert.service';
import { PatientService } from './../_services/patient.service';
import { PatientEditComponent } from './patient-edit/patient-edit.component';
import { Patient } from './../_models/patient';
import { Anamnesis } from './../_models/anamnesis';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent implements OnInit {
  public patients: Patient[];
  public search = '';
  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    public patientService: PatientService,
    public toastrService: ToastrAlertService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.patients = data.patients;
    });
  }
  openDialog(currentPatient: Patient): void {
    const dialogRef = this.dialog.open(PatientEditComponent, {
      data: { patient: currentPatient }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(currentPatient);
    });
  }
  updatePatient(patient: Patient) {
    this.patientService.updatePatient(this.authService.decodedToken.nameid, patient.id, patient).subscribe(() => {
       this.toastrService.success(`Данные ${patient.fullname} пациента успешно обновлены`); }
    , err => {
      this.toastrService.error(err);
    });
  }
  createPatient(patient: Patient) {
    this.patientService.createPatient(this.authService.decodedToken.nameid, patient).subscribe(() => {
      this.toastrService.success(`Пациент ${patient.fullname} успешно добавлен в систему`);
    }, err => {
      this.toastrService.error(err);
    });
  }
}
