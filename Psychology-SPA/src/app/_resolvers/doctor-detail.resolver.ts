import { ToastrAlertService } from '../_services/toastr-alert.service';
import { DoctorService } from '../_services/doctor.service';
import { Doctor } from '../_models/doctor';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()

export class DoctorDetailResolver implements Resolve<Doctor> {
    constructor(private doctorService: DoctorService, private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Doctor> {
        return this.doctorService.getDoctor(route.params.id).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id']);
                return of(null);
            })
        );
    }
}
