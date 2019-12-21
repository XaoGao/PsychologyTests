import { PhonebookService } from './../../_services/phonebook.service';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Phone } from './../../_models/phone';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ModalPhoneComponent } from './modal-phone/modal-phone.component';

@Component({
  selector: 'app-phone',
  templateUrl: './phone.component.html',
  styleUrls: ['./phone.component.css']
})
export class PhoneComponent implements OnInit {
  public phones: Phone[];
  displayedColumns: string[] = ['position', 'numberMask', 'isLock', 'edit'];
  dataSource = new MatTableDataSource(this.phones);
  constructor(private toastrService: ToastrAlertService,
              private route: ActivatedRoute,
              private phonebookService: PhonebookService,
              public dialog: MatDialog) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.phones = data.phones;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(currentPhone?: Phone): void {
    if (currentPhone === null) {
      const dialogRef = this.dialog.open(ModalPhoneComponent, {
        width: '600px',
        data: { phone: new Phone()}
      });
      dialogRef.afterClosed().subscribe(result => {
    //     // if (result) {
    //     //   this.createTodo(result as Todo);
    //     //   this.todoListService.activeTodoList.todos.push(result);
    //     // }
      });
    } else {
      const dialogRef = this.dialog.open(ModalPhoneComponent, {
        width: '600px',
        data: { phone: currentPhone }
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.updatePhone(result as Phone);
        }
      });
    }
  }
  updatePhone(phone: Phone) {
    this.phonebookService.updatePhone(phone.id, phone).subscribe(
      (res) => {
        this.toastrService.success('Данные успешно обновлены');
      }, err => {
        this.toastrService.error(err);
      }
    );
  }
}
