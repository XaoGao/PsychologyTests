import { Passwords } from './../_models/passwords';
import { Reception } from './../_models/reception';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private BASE_URL = environment.apiUrl + 'auth/';
  public helper = new JwtHelperService();
  public decodedToken: any;
  receptions: Reception[];

  private admin = 'admin';
  private HR = 'hr';
  private doctor = 'doctor';
  private registry = 'registry';

  public doctorId: number;
  public role: string;
  public username: string;

  constructor(private http: HttpClient) { }

  public login(model: any) {
    return this.http.post(this.BASE_URL + 'login', model).pipe(
      map((res: any) => {
        const user = res;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('receptions', JSON.stringify(user.receptionsForReturn));
          this.decodedToken = this.helper.decodeToken(user.token);
          this.setUserDetail(this.decodedToken);
          this.receptions = user.receptionsForReturn;
          // !раскомментировать, чтобы посмотреть, что лежит в токене
          // console.log(this.decodedToken);
        }
      })
    );
  }
  public register(model: any) {
    return this.http.post(this.BASE_URL + 'register', model);
  }
  public loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.helper.isTokenExpired(token);
  }
  public loggedOut(): void {
    this.decodedToken = null;
    localStorage.removeItem('token');
    localStorage.removeItem('receptions');
  }
  public setUserDetail(decodedToken: any) {
    this.doctorId = decodedToken.nameid;
    this.role = decodedToken.role;
    this.username = decodedToken.unique_name;
  }

  public changePassword(doctorId: number, passwords: Passwords) {
    return this.http.put(this.BASE_URL + doctorId + '/changePassword', passwords );
  }

  public isAdmin(): boolean {
    if (this.role === this.admin) {
      return true;
    } else {
      return false;
    }
  }
  public isHR(): boolean {
    if (this.role === this.HR) {
      return true;
    } else {
      return false;
    }
  }
  public isDoctor(): boolean {
    if (this.role === this.doctor) {
      return true;
    } else {
      return false;
    }
  }
  public isRegistry(): boolean {
    if (this.role === this.registry) {
      return true;
    } else {
      return false;
    }
  }

}
