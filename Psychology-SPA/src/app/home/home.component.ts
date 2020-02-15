import { Router, ActivatedRoute } from '@angular/router';
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

  constructor(private authService: AuthService, private toastrService: ToastrAlertService,
              private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {

  }

  public login() {
    this.authService.login(this.userForLogin).subscribe(() => {
      this.toastrService.success('Вы успешно зашли в программу');
      this.router.navigate(['/workship/' + this.authService.decodedToken.nameid]);
    }, err => {
      this.toastrService.error(err);
    });
  }
  public register() {
    this.authService.login(this.userForRegister).subscribe(() => {
      this.toastrService.success('Вы успешно зарегистрировались в программе');
    }, err => {
      this.toastrService.error(err);
    });
  }

}
