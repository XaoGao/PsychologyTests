import { TestService } from '../_services/test.service';
import { PhonebookService } from '../_services/phonebook.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Test } from '../_models/test';

@Injectable()

export class TestsResolver implements Resolve<Test[]> {
    constructor(private authService: AuthService, private testService: TestService ,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Test[]> {
        return this.testService.getTests().pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id']);
                return of(null);
            })
        );
    }
}
