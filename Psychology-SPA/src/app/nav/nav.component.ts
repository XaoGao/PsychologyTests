import { DocumentService } from './../_services/document.service';
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
  userForLogin: any = { };
  constructor(public authService: AuthService,
              private route: Router,
              private toastrService: ToastrAlertService,
              private documentService: DocumentService,
              private router: Router) { }

  ngOnInit() {
  }
  public login() {
    this.authService.login(this.userForLogin).subscribe(() => {
      this.toastrService.success('Вы успешно зашли в программу');
      this.router.navigate(['/workship/' + this.authService.doctorId]);
    }, err => {
      this.toastrService.error(err);
    });
  }
  public loggedin(): boolean {
    return this.authService.loggedIn();
  }
  public logout() {
    this.authService.loggedOut();
    this.toastrService.info('Вы вышли из системы');
    this.route.navigate(['/']);
  }
  public isHR(): boolean {
    return this.authService.isHR();
  }
  public isAdmin(): boolean {
    return this.authService.isAdmin();
  }
  public isRegistry(): boolean {
    return this.authService.isRegistry();
  }
  public setLocal(): void {
    this.documentService.changeInterdepart('local').subscribe(() => {
      this.documentService.realInterdepartType = false;
    }, err => {
      this.toastrService.error(err);
    });
  }
  public setReal(): void {
    this.documentService.changeInterdepart('real').subscribe(() => {
      this.documentService.realInterdepartType = true;
    }, err => {
      this.toastrService.error(err);
    });
  }
  public interdepartType(): boolean {
    return this.documentService.realInterdepartType;
  }
}
