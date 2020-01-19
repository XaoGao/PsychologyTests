import { PatientService } from './../_services/patient.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Patient } from '../_models/patient';

@Injectable()

export class PatientsListForRegistryResolver implements Resolve<Patient[]> {
    constructor(private authService: AuthService, private patientService: PatientService,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Patient[]> {
        return this.patientService.getPatientsForRegistry(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id', this.authService.decodedToken.nameid]);
                return of(null);
            })
        );
    }
}
