import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Department } from 'src/app/_models/department';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  public departments: Department[];
  displayedColumns: string[] = ['position', 'name', 'sortLevel', 'isLock'];
  dataSource = new MatTableDataSource(this.departments);
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.departments = data.departments;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}

