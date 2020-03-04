import { AdminService } from './../_services/admin.service';
import { Doctor } from './../_models/doctor';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Injectable()

export class DoctorsListForAdminResolver implements Resolve<Doctor[]> {
    constructor(private authService: AuthService, private adminService: AdminService,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Doctor[]> {
        return this.adminService.getDoctors(this.authService.doctorId).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship', this.authService.doctorId]);
                return of(null);
            })
        );
    }
}
