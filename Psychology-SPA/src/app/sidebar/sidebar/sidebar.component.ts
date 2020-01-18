import { RolesService } from './../../_services/roles.service';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Doctor } from './../../_models/doctor';
import { AuthService } from './../../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  doctor: Doctor;
  constructor(public authService: AuthService,
              private toastrService: ToastrAlertService,
              private rolesService: RolesService) { }

  ngOnInit() {
  }
  logged(): boolean {
    return this.authService.loggedIn();
  }
  ShowMeError() {
    this.toastrService.error('err');
  }
  isDoctor(): boolean {
    return this.rolesService.isDoctor(this.authService.decodedToken.role);
  }
  isHR(): boolean {
    return this.rolesService.isHR(this.authService.decodedToken.role);
  }
  isRegistry(): boolean {
    return this.rolesService.isRegistry(this.authService.decodedToken.role);
  }
}
