import { DocService } from './../_services/doc.service';
import { ToastrAlertService } from '../_services/toastr-alert.service';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Injectable()

export class DocumentsListResolver implements Resolve<Document> {
    constructor(private authService: AuthService, private docService: DocService,
                private router: Router, private toastrService: ToastrAlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Document> {
        return this.docService.getDocuments(this.authService.decodedToken.nameid, route.params.id).pipe(
            catchError(error => {
                this.toastrService.error('Ошибка при загрузке данных');
                this.router.navigate(['/workship/:id', this.authService.decodedToken.nameid]);
                return of(null);
            })
        );
    }
}
