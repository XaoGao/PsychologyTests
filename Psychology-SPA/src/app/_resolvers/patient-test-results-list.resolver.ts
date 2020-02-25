import { TestService } from '../_services/test.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { PatientTestResult } from '../_models/patientTestResults';

@Injectable()

export class PatientTestResultsListResolver implements Resolve<PatientTestResult[]> {
    constructor(private authService: AuthService, private testService: TestService ,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PatientTestResult[]> {
        return this.testService.getTestsHistory(this.authService.doctorId, route.params.id).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id', this.authService.doctorId]);
                return of(null);
            })
        );
    }
}
