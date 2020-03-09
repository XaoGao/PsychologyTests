import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Role } from '../_models/role';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  roles: Role[];
  displayedColumns: string[] = ['№', 'name', 'isLock'];
  dataSource = new MatTableDataSource<Role>(this.roles);
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.roles = data.roles;
    });
    this.dataSource = new MatTableDataSource<Role>(this.roles);
  }
  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
