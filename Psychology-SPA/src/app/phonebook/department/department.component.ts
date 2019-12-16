import { Department } from './../../_models/department';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ModalDepartmentComponent } from './modal-department/modal-department.component';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  public departments: Department[];
  displayedColumns: string[] = ['position', 'name', 'sortLevel', 'isLock', 'edit'];
  dataSource = new MatTableDataSource(this.departments);
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute, public dialog: MatDialog) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.departments = data.departments;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(currentDepartment?: Department): void {
    // console.log(currentDepartment);
    if (currentDepartment === null) {
      const dialogRef = this.dialog.open(ModalDepartmentComponent, {
        width: '600px',
        data: { department: new Department()}
      });
      dialogRef.afterClosed().subscribe(result => {
    //     // if (result) {
    //     //   this.createTodo(result as Todo);
    //     //   this.todoListService.activeTodoList.todos.push(result);
    //     // }
      });
    } else {
      const dialogRef = this.dialog.open(ModalDepartmentComponent, {
        width: '600px',
        data: { department: currentDepartment }
      });
      dialogRef.afterClosed().subscribe(result => {
    //     // if (result) {
    //     //   this.updateTodo(result);
        // }
      });
    }
  }



}

