import { Phone } from './../_models/phone';
import { PhonebookService } from './../_services/phonebook.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Injectable()

export class PhonesResolver implements Resolve<Phone> {
    constructor(private authService: AuthService, private phonebookService: PhonebookService,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Phone> {
        return this.phonebookService.getPhones().pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id', this.authService.doctorId]);
                return of(null);
            })
        );
    }
}
