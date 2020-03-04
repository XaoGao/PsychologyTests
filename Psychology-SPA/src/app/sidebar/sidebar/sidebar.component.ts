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
              private toastrService: ToastrAlertService) { }

  ngOnInit() {
  }
  public isDoctor(): boolean {
    return this.authService.isDoctor();
  }
  public isHR(): boolean {
    return this.authService.isHR();
  }
  public isRegistry(): boolean {
    return this.authService.isRegistry();
  }
  public isAdmin(): boolean {
    return this.authService.isAdmin();
  }
}
