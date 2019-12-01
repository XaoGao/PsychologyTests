import { ToastrAlertService } from './../_services/toastr-alert.service';
import { Component, OnInit } from '@angular/core';
import { Doctor } from '../_models/doctor';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-doctor-edit',
  templateUrl: './doctor-edit.component.html',
  styleUrls: ['./doctor-edit.component.css']
})
export class DoctorEditComponent implements OnInit {
  public doctor: Doctor;
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.doctor = data.doctor;
    })
  }
} 