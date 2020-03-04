import { VacationService } from './../_services/vacation.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Vacation } from '../_models/vacation';

@Injectable()

export class VacationsListForDoctorResolver implements Resolve<Vacation[]> {
    constructor(private authService: AuthService, private vacationService: VacationService,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Vacation[]> {
        return this.vacationService.getVacationsForDoctor(this.authService.doctorId).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship', this.authService.doctorId]);
                return of(null);
            })
        );
    }
}
