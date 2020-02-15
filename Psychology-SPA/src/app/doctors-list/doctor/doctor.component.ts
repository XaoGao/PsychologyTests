import { Vacation } from './../../_models/vacation';
import { Phone } from './../../_models/phone';
import { Department } from 'src/app/_models/department';
import { Doctor } from './../../_models/doctor';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Position } from './../../_models/position';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {

  doctor: Doctor;
  public departments: Department[];
  public positions: Position;
  public phones: Phone[];
  public vacations: Vacation[];
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.doctor = data.doctor;
      this.departments = data.departments;
      this.positions = data.positions;
      this.phones = data.phones;
    });
    console.log(this.doctor);
  }
  public isNewDoctor(): boolean {
    if (this.doctor.id) {
      return false;
    } else {
      return true;
    }
  }
  public updateDoctor(): void {
    console.log(this.doctor);
  }
}
