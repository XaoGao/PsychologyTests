import { Component, OnInit } from '@angular/core';
import { Doctor } from '../_models/doctor';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { ActivatedRoute } from '@angular/router';
import { DoctorService } from '../_services/doctor.service';

@Component({
  selector: 'app-doctor-detail',
  templateUrl: './doctor-detail.component.html',
  styleUrls: ['./doctor-detail.component.css']
})
export class DoctorDetailComponent implements OnInit {
  public doctor: Doctor;
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute, private doctorService: DoctorService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.doctor = data.doctor;
    });
  }
  updateDoctor() {
    this.doctorService.updateDoctor(this.doctor).subscribe(() => {
      this.toastrService.success('Дданные успешно обновлены');
    }, err => {
      this.toastrService.error(err);
    });
  }
}
