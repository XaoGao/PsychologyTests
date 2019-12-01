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
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      console.log(this.departmentsWithDoctors);
      this.departmentsWithDoctors = data.departmentsWithDoctors;
    });
  }

}
