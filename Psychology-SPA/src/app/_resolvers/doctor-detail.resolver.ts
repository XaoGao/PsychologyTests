import { ToastrAlertService } from '../_services/toastr-alert.service';
import { DoctorService } from '../_services/doctor.service';
import { Doctor } from '../_models/doctor';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Injectable()

export class DoctorDetailResolver implements Resolve<Doctor> {
    constructor(private doctorService: DoctorService, private router: Router,
                private toastrService: ToastrAlertService, private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Doctor> {
        return this.doctorService.getDoctor(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id', this.authService.decodedToken.nameid]);
                return of(null);
            })
        );
    }
}
