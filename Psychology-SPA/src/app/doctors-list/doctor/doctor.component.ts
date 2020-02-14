import { Doctor } from './../../_models/doctor';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {

  doctor: Doctor[];
  constructor(route: ActivatedRoute) { }

  ngOnInit() {
  }

}
