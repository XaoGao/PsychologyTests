import { AuthService } from './../_services/auth.service';
import { Reception } from './../_models/reception';
import { ActivatedRoute, Router } from '@angular/router';
import { Patient } from './../_models/patient';
import { Doctor } from './../_models/doctor';
import { Component, OnInit } from '@angular/core';
import { ReceptionService } from '../_services/reception.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';

@Component({
  selector: 'app-receptions',
  templateUrl: './receptions.component.html',
  styleUrls: ['./receptions.component.css']
})
export class ReceptionsComponent implements OnInit {

  public doctors: Doctor[];
  public patients: Patient[];
  public currentDoctor: Doctor;
  public currentPatient: Patient;
  public date: Date;
  public times: Date[];
  constructor(private route: ActivatedRoute,
              private receptionService: ReceptionService,
              private toastrService: ToastrAlertService,
              private router: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.doctors = data.doctors;
      this.currentDoctor = this.doctors[0];
    });
  }
  public getFreeTime(): void {
    this.receptionService.getFreeTime(this.currentDoctor.id, this.date).subscribe((data) => {
      this.times = data;
    }, err => {
      this.toastrService.error(err);
    });
  }
  public toMakeAnAppointment(timeReception: Date): void {
    const reception = new Reception();
    reception.doctorId = this.currentDoctor.id;
    reception.patientId = this.currentPatient.id;
    reception.dateTimeReception = timeReception;
    this.receptionService.createReception(reception).subscribe(() => {
      this.toastrService.success(`${this.currentPatient.fullname} записан на ${timeReception.toLocaleString()}`);
      // this.router.navigate(['/workship/:id', this.authService.decodedToken.nameid]);
    }, err => {
      this.toastrService.error(err);
    });
  }
}
