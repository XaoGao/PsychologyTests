import { AuthService } from './../../_services/auth.service';
import { AdminService } from './../../_services/admin.service';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Phone } from './../../_models/phone';
import { Department } from 'src/app/_models/department';
import { Doctor } from './../../_models/doctor';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Position } from './../../_models/position';
import { Role } from './../../_models/role';

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
  public roles: Role[];
  constructor(private route: ActivatedRoute,
              private adminService: AdminService,
              private toastrService: ToastrAlertService,
              private authService: AuthService,
              private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.doctor = data.doctor;
      this.departments = data.departments;
      this.positions = data.positions;
      this.phones = data.phones;
      this.roles = data.roles;
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
  public save(): void {
    if (this.isNewDoctor()) {
      this.createDoctor();
    } else {
      this.updateDoctor();
    }
  }
  private createDoctor(): void {
    this.adminService.createDoctor(this.authService.doctorId, this.doctor).subscribe(() => {
      this.toastrService.success('Вы добавили нового врача');
      this.router.navigate(['/doctors']);
    }, err => {
      this.toastrService.error(err);
    });
  }
  private updateDoctor(): void {
    this.adminService.updateDoctor(this.authService.doctorId, this.doctor).subscribe(() => {
      this.toastrService.success('Вы обновили врача');
      this.router.navigate(['/doctors']);
    }, err => {
      this.toastrService.error(err);
    });
  }
}
