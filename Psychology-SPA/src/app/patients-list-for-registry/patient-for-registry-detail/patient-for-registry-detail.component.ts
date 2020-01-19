import { AuthService } from './../../_services/auth.service';
import { Patient } from './../../_models/patient';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-patient-for-registry-detail',
  templateUrl: './patient-for-registry-detail.component.html',
  styleUrls: ['./patient-for-registry-detail.component.css']
})
export class PatientForRegistryDetailComponent implements OnInit {
  public patient: Patient;
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  public isNewPatient(): boolean {
    if (this.patient.id === undefined) {
      this.patient.doctorId = this.authService.decodedToken.nameid;
      return true;
    } else {
      return false;
    }
  }
}
