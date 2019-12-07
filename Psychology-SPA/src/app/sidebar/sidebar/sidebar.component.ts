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
  constructor(public authService: AuthService, private toastrService: ToastrAlertService) { }

  ngOnInit() {
  }
  logged(): boolean {
    return this.authService.loggedIn();
  }
  ShowMeError() {
    this.toastrService.error('err');
  }
}
