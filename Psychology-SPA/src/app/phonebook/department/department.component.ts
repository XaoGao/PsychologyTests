import { AuthService } from './../../_services/auth.service';
import { PhonebookService } from './../../_services/phonebook.service';
import { Department } from './../../_models/department';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ModalDepartmentComponent } from './modal-department/modal-department.component';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  public departments: Department[];
  displayedColumns: string[] = ['position', 'name', 'sortLevel', 'isLock', 'edit'];

  dataSource = new MatTableDataSource<Department>(this.departments);
  constructor(private toastrService: ToastrAlertService,
              private route: ActivatedRoute,
              public dialog: MatDialog,
              private phonebookService: PhonebookService,
              private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.departments = data.departments;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  addDepartment() {
    this.openDialog();
  }
  editDepartment(department: Department) {
    this.openDialog(department);
  }

  openDialog(currentDepartment?: Department): void {
    if (!currentDepartment) {
      const dialogRef = this.dialog.open(ModalDepartmentComponent, {
        width: '600px',
        data: { department: new Department()}
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.createDepartment(result as Department);
        }
      });
    } else {
      const dialogRef = this.dialog.open(ModalDepartmentComponent, {
        width: '600px',
        data: { department: currentDepartment }
      });
      dialogRef.afterClosed().subscribe(result => {
         if (result) {
           this.updateDepartment(result as Department);
        }
      });
    }
  }

  updateDepartment(department: Department) {
    this.phonebookService.updateDepartment(department.id, department).subscribe(
      () => {
        this.toastrService.success('Данные успешно обновлены');
      }, err => {
        this.toastrService.error(err);
      }
    );
  }
  createDepartment(department: Department) {
    this.phonebookService.createDepartment(this.authService.decodedToken.nameid, department).subscribe((res: Department) => {
        this.toastrService.success('Новый отдел успешно добавлен');
      }, err => {
        this.toastrService.error(err);
      }
    );
  }
}

