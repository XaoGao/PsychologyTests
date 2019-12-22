import { AuthService } from './../../_services/auth.service';
import { PhonebookService } from './../../_services/phonebook.service';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Position } from './../../_models/position';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ModalPositionComponent } from './modal-position/modal-position.component';

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.css']
})
export class PositionComponent implements OnInit {
  public positions: Position[];
  displayedColumns: string[] = ['position', 'name', 'sortLevel', 'isLock', 'edit'];
  dataSource = new MatTableDataSource(this.positions);
  constructor(private toastrService: ToastrAlertService,
              private route: ActivatedRoute,
              private phonebookService: PhonebookService,
              public dialog: MatDialog,
              private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.positions = data.positions;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  addPosition() {
    this.openDialog();
  }
  editPosition(position: Position) {
    this.openDialog(position);
  }
  openDialog(currentPosition?: Position): void {
    if (!currentPosition) {
      const dialogRef = this.dialog.open(ModalPositionComponent, {
        width: '600px',
        data: { position: new Position()}
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.createPosition(result as Position);
          this.positions.push(result);
        }
      });
    } else {
      const dialogRef = this.dialog.open(ModalPositionComponent, {
        width: '600px',
        data: { position: currentPosition }
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
      this.updatePosition(result as Position);
        }
      });
    }
  }
  updatePosition(position: Position) {
    this.phonebookService.updatePosition(position.id, position).subscribe(
      (res) => {
        this.toastrService.success('Данные успешно обновлены');
      }, err => {
        this.toastrService.error(err);
      }
    );
  }
  createPosition(position: Position) {
    this.phonebookService.createPosition(this.authService.decodedToken.nameid, position).subscribe(
      () => {
        this.toastrService.success('Новая должность успешно добавлена');
      }, err => {
        this.toastrService.error(err);
      }
    );
  }
}
