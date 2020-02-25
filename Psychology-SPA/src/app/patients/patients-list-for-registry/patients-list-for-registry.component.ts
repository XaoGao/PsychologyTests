import { Patient } from '../../_models/patient';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { PatientService } from '../../_services/patient.service';
import { ToastrAlertService } from '../../_services/toastr-alert.service';

@Component({
  selector: 'app-patients-list-for-registry',
  templateUrl: './patients-list-for-registry.component.html',
  styleUrls: ['./patients-list-for-registry.component.css']
})
export class PatientsListForRegistryComponent implements OnInit {

  public patients: Patient[];
  public search = '';
  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    public patientService: PatientService,
    public toastrService: ToastrAlertService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.patients = data.patients;
    });
  }
  public deletePatient(patient: Patient): void {
    if (confirm(`Вы уверены, что хотите удалить пациента с картой № ${patient.personalCardNumber}?`)) {
      this.patientService.deletePatient(this.authService.doctorId, patient.id).subscribe(() => {
        this.toastrService.info(`Пациент с номером картой ${patient.personalCardNumber} переведен в архив`);
        const index = this.patients.indexOf(patient, 0);
        if (index > -1) {
          this.patients.splice(index, 1);
        }
      }, err => {
        this.toastrService.error(err);
      });
    }
  }
}
