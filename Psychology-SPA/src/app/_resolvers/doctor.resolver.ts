import { AdminService } from './../_services/admin.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Doctor } from '../_models/doctor';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Injectable()

export class DoctorResolver implements Resolve<Doctor> {
    constructor(private adminService: AdminService, private router: Router,
                private toastrService: ToastrAlertService, private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Doctor> {
        return this.adminService.getDoctor(this.authService.doctorId, route.params.id).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship', this.authService.doctorId]);
                return of(null);
            })
        );
    }
}
