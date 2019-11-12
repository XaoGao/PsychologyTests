import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  helper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login() {

  }
  register() {

  }
  loggedIn(): boolean {
    return true;
  }
  loggedOut(): void {
    this.decodedToken = null;
    localStorage.removeItem('token');
  }
}
