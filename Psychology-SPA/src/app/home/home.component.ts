import { ToastrAlertService } from './../_services/toastr-alert.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userForLogin: any = { };
  userForRegister: any = { };
  constructor(private authService: AuthService, private toastrService: ToastrAlertService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.userForLogin).subscribe(() => {
      this.toastrService.success('Вы успешно зашли в программу');
    }, err => {
      this.toastrService.error(err);
    });
  }
  register() {
    this.authService.login(this.userForLogin).subscribe(() => {
      this.toastrService.success('Вы успешно заиегистрировались в программу');
    }, err => {
      this.toastrService.error(err);
    });
  }
}
