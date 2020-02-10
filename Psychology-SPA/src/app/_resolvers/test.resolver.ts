import { TestService } from '../_services/test.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Test } from '../_models/test';

@Injectable()

export class TestResolver implements Resolve<Test> {
    constructor(private authService: AuthService, private testService: TestService ,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Test> {
        return this.testService.getTest(this.authService.decodedToken.nameid, route.params.id, route.params.testId).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id', this.authService.decodedToken.nameid]);
                return of(null);
            })
        );
    }
}
