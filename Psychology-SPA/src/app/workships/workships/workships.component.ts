import { AuthService } from './../../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-workships',
  templateUrl: './workships.component.html',
  styleUrls: ['./workships.component.css']
})
export class WorkshipsComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  logged(): boolean {
    return this.authService.loggedIn();
  }
}
