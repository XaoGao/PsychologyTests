import { ToastrAlertService } from './../_services/toastr-alert.service';
import { PatientService } from './../_services/patient.service';
import { Patient } from './../_models/patient';
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
  // TODO: delete
  // public addPatient() {
  //   this.openDialog();
  // }
  // public editPatient(patient: Patient) {
  //   this.openDialog(patient);
  // }

  // private openDialog(currentPatient?: Patient): void {
  //   if (!currentPatient) {
  //     const dialogRef = this.dialog.open(PatientEditComponent, {
  //       data: { patient: new Patient() }
  //     });
  //     dialogRef.afterClosed().subscribe(result => {
  //       if (result) {
  //         this.createPatient(result as Patient);
  //       }
  //     });
  //   } else {
  //     const dialogRef = this.dialog.open(PatientEditComponent, {
  //       data: { patient: currentPatient }
  //     });
  //     dialogRef.afterClosed().subscribe(result => {
  //       if (result) {
  //         this.updatePatient(result as Patient);
  //       }
  //     });
  //   }
  // }
  // private updatePatient(patient: Patient) {
  //   this.patientService.updatePatient(this.authService.decodedToken.nameid, patient.id, patient).subscribe((res) => {
  //      const newpatient = res as Patient;
  //      this.toastrService.success(`Данные ${newpatient.fullname} пациента успешно обновлены`);
  //      const index = this.patients.indexOf(patient, 0);
  //      if (index > -1) {
  //        this.patients.splice(index, 1);
  //        this.patients.push(newpatient as Patient);
  //      }
  //     }
  //   , err => {
  //     this.toastrService.error(err);
  //   });
  // }
  // private createPatient(patient: Patient) {
  //   this.patientService.createPatient(this.authService.decodedToken.nameid, patient).subscribe((res) => {
  //     const newpatient = res as Patient;
  //     this.toastrService.success(`Пациент ${newpatient.fullname} успешно добавлен в систему`);
  //     this.patients.push(newpatient);
  //   }, err => {
  //     this.toastrService.error(err);
  //   });
  // }
}
