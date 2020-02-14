import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Role } from '../_models/role';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  roles: Role[];
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.roles = data.roles;
    });
  }

}
