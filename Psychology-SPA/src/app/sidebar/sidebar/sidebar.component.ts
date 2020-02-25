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
  public isDoctor(): boolean {
    return this.rolesService.isDoctor(this.authService.role);
  }
  public isHR(): boolean {
    return this.rolesService.isHR(this.authService.role);
  }
  public isRegistry(): boolean {
    return this.rolesService.isRegistry(this.authService.role);
  }
  public isAdmin(): boolean {
    return this.rolesService.isAdmin(this.authService.role);
  }
}
