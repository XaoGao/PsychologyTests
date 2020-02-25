import { DepartmentWithDoctors } from './../_models/departmentWithDoctors';
import { ToastrAlertService } from './../_services/toastr-alert.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-phonebook',
  templateUrl: './phonebook.component.html',
  styleUrls: ['./phonebook.component.css']
})
export class PhonebookComponent implements OnInit {
  public departmentsWithDoctors: DepartmentWithDoctors[];
  public index: number;
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.departmentsWithDoctors = data.departmentsWithDoctors;
    });
    this.index = 0;
  }

  // public getIndex(): number {
  //   this.index = this.index + 1;
  //   return this.index;
  // }

}
