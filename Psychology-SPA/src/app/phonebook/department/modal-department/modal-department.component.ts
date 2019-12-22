import { Department } from './../../../_models/department';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-department',
  templateUrl: './modal-department.component.html',
  styleUrls: ['./modal-department.component.css']
})
export class ModalDepartmentComponent implements OnInit {
  public department: Department;
  constructor(
    public dialogRef: MatDialogRef<ModalDepartmentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    if (this.data.department) {
      this.department = this.data.department;
    } else {
      this.department = new Department();
    }
  }
}
