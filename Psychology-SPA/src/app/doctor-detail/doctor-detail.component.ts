import { Phone } from './../_models/phone';
import { Position } from './../_models/position';
import { Component, OnInit } from '@angular/core';
import { Doctor } from '../_models/doctor';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { ActivatedRoute } from '@angular/router';
import { DoctorService } from '../_services/doctor.service';
import { Department } from '../_models/department';

@Component({
  selector: 'app-doctor-detail',
  templateUrl: './doctor-detail.component.html',
  styleUrls: ['./doctor-detail.component.css']
})
export class DoctorDetailComponent implements OnInit {
  public doctor: Doctor;
  departments: Department[];
  positions: Position[];
  phones: Phone[];
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute, private doctorService: DoctorService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.doctor = data.doctor;
      this.departments = data.departments;
      this.positions = data.positions;
      this.phones = data.phones;
    });
  }
  updateDoctor() {
    this.doctorService.updateDoctor(this.doctor).subscribe(() => {
      this.toastrService.success('Данные успешно обновлены');
    }, err => {
      this.toastrService.error(err);
    });
  }
}
