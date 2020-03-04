import { TestService } from '../_services/test.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { PatientTestResult } from '../_models/patientTestResults';

@Injectable()

export class PatientTestResultsDetailResolver implements Resolve<PatientTestResult> {
    constructor(private authService: AuthService, private testService: TestService ,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PatientTestResult> {
        return this.testService.getTestHistory(this.authService.doctorId, route.params.id, route.params.testhistoryId).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship', this.authService.doctorId]);
                return of(null);
            })
        );
    }
}
