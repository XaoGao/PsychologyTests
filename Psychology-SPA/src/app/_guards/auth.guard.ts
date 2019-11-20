import { ToastrAlertService } from './../_services/toastr-alert.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private toastrAlertService: ToastrAlertService, private router: Router) { }

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }
    this.toastrAlertService.error('Войдите в систему');
    this.router.navigate(['/']);
    return false;
  }
}
