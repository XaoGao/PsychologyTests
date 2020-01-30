import { ActivatedRoute, Data } from '@angular/router';
import { Patient } from './../_models/patient';
import { Doctor } from './../_models/doctor';
import { Component, OnInit } from '@angular/core';

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
  public data: Data;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.doctors = data.doctors;
      this.currentDoctor = this.doctors[0];
    });
  }
  public test(): void {
    console.log(this.currentDoctor);
  }
}
