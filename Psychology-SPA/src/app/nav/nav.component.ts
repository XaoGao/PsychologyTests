import { ToastrAlertService } from './../_services/toastr-alert.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService, private route: Router, private toastrService: ToastrAlertService) { }

  ngOnInit() {
    console.log(this.authService.decodedToken);
  }
  loggedin(): boolean {
    return this.authService.loggedIn();
  }
  logout() {
    this.authService.loggedOut();
    this.toastrService.info('Вы вышли из системы');
    this.route.navigate(['/']);
  }
}
