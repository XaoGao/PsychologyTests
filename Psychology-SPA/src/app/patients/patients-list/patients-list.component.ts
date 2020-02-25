import { ToastrAlertService } from '../../_services/toastr-alert.service';
import { PatientService } from '../../_services/patient.service';
import { Patient } from '../../_models/patient';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
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
}
