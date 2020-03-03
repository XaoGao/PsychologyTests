import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { AdminService } from './../../_services/admin.service';
import { AuthService } from './../../_services/auth.service';
import { Doctor } from '../../_models/doctor';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, throwMatDialogContentAlreadyAttachedError } from '@angular/material';

@Component({
  selector: 'app-doctors-list',
  templateUrl: './doctors-list.component.html',
  styleUrls: ['./doctors-list.component.css']
})
export class DoctorsListComponent implements OnInit {

  doctors: Doctor[];
  search = '';
  displayedColumns: string[] = ['position', 'username', 'fullname', 'isLock', 'edit', 'delete', 'dropPassword'];
  dataSource = new MatTableDataSource<Doctor>(this.doctors);
  constructor(private route: ActivatedRoute,
              private authService: AuthService,
              private adminService: AdminService,
              private toastrService: ToastrAlertService) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.doctors = data.doctors;
    });
    this.dataSource = new MatTableDataSource<Doctor>(this.doctors);
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  public deleteDoctor(doctorId: number): void {
    if (confirm('Вы уверены, что хотите заблокировать пользователя?')) {
      this.adminService.deleteDoctor(this.authService.doctorId, doctorId).subscribe(() => {
        this.adminService.deleteDoctor(this.authService.doctorId, doctorId);
        this.toastrService.success('Пользователен заблокирован');
        const doctor = this.doctors.find((element, index) => {
          if (element.id === doctorId) {
            if (element) {
              return true;
            } else {
              return false;
            }
          }
        });
        doctor.isLock = true;
      }, err => {
        this.toastrService.error(err);
      });
    }
  }
  public dropPassword(doctorId: number): void {
    if (confirm('Вы уверены, что хотите сбросить пароль?')) {
      this.adminService.dropPassword(this.authService.doctorId, doctorId).subscribe(() => {
        this.toastrService.info('Пароль сброшен');
      }, err => {
        this.toastrService.error(err);
      });
    }
  }
}
