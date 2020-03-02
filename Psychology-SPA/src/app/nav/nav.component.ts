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

  constructor(public authService: AuthService,
              private route: Router,
              private toastrService: ToastrAlertService,
              private documentService: DocumentService) { }

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
    return this.authService.isHR(this.authService.role);
  }
  public isAdmin(): boolean {
    return this.authService.isAdmin(this.authService.role);
  }
  public isRegistry(): boolean {
    return this.authService.isRegistry(this.authService.role);
  }
  public setLocal(): void {
    this.documentService.changeInterdepart('local');
  }
  public setReal(): void {
    this.documentService.changeInterdepart('real');
  }
}
