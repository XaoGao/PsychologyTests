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
  public search: string;
  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.patients = data.patients;
    });
  }
  openDialog(currentPatient: Patient): void {
    const dialogRef = this.dialog.open(PatientEditComponent, {
      width: '600px',
      data: { patient: currentPatient }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(currentPatient);
    });
  }
}
