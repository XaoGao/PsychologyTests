import { AuthService } from './../../_services/auth.service';
import { Patient } from './../../_models/patient';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-patient-edit',
  templateUrl: './patient-edit.component.html',
  styleUrls: ['./patient-edit.component.css']
})
export class PatientEditComponent implements OnInit {
  public patient: Patient;
  constructor(
    public authService: AuthService,
    public dialogRef: MatDialogRef<PatientEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.patient = this.data.patient;
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  isNewPatient(): boolean {
    if (this.patient.id === undefined) {
      this.patient.doctorId = this.authService.decodedToken.nameid;
      return true;
    } else {
      return false;
    }
  }
}
