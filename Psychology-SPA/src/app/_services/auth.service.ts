import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  BASE_URL = environment.apiUrl + 'auth/';
  helper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.BASE_URL + 'login', model).pipe(
      map((res: any) => {
        const user = res;
        console.log(user);
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('receptions', JSON.stringify(user.receptionsForWeekForDoctor));
          this.decodedToken = this.helper.decodeToken(user.token);
          // !раскомментировать, чтобы посмотреть, что лежит в токене
          // console.log(this.decodedToken);
        }
      })
    );
  }
  register(model: any) {
    return this.http.post(this.BASE_URL + 'register', model);
  }
  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.helper.isTokenExpired(token);
  }
  loggedOut(): void {
    this.decodedToken = null;
    localStorage.removeItem('token');
  }
}
