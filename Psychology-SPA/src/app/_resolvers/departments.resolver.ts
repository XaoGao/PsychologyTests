import { PhonebookService } from './../_services/phonebook.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Department } from '../_models/department';
import { AuthService } from '../_services/auth.service';

@Injectable()

export class DepartmentsResolver implements Resolve<Department> {
    constructor(private authService: AuthService, private phonebookService: PhonebookService,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Department> {
        return this.phonebookService.getDepartments(true).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id']);
                return of(null);
            })
        );
    }
}
