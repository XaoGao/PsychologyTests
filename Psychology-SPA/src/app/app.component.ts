import { AuthService } from './_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const receptions = JSON.parse(localStorage.getItem('receptions'));
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
      this.authService.setUserDetail(this.authService.decodedToken);
    }
    if (receptions) {
      this.authService.receptions = receptions;
    }
  }
  logged(): boolean {
    return this.authService.loggedIn();
  }
}
