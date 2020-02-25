import { DocService } from './../_services/doc.service';
import { RolesService } from './../_services/roles.service';
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

  constructor(public authService: AuthService,
              private route: Router,
              private toastrService: ToastrAlertService,
              private rolseService: RolesService,
              private documentService: DocService) { }

  ngOnInit() {
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
    return this.rolseService.isHR(this.authService.role);
  }
  public isAdmin(): boolean {
    return this.rolseService.isAdmin(this.authService.role);
  }
  public isRegistry(): boolean {
    return this.rolseService.isRegistry(this.authService.role);
  }
  public setLocal(): void {
    this.documentService.changeInterdepart('local');
  }
  public setReal(): void {
    this.documentService.changeInterdepart('real');
  }
}
