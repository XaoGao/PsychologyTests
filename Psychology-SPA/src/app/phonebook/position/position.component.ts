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
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute, public dialog: MatDialog) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.positions = data.positions;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(currentPosition?: Position): void {
    // console.log(currentPosition);
    if (currentPosition === null) {
      const dialogRef = this.dialog.open(ModalPositionComponent, {
        width: '600px',
        data: { position: new Position()}
      });
      dialogRef.afterClosed().subscribe(result => {
    //     // if (result) {
    //     //   this.createTodo(result as Todo);
    //     //   this.todoListService.activeTodoList.todos.push(result);
    //     // }
      });
    } else {
      const dialogRef = this.dialog.open(ModalPositionComponent, {
        width: '600px',
        data: { position: currentPosition }
      });
      dialogRef.afterClosed().subscribe(result => {
    //     // if (result) {
    //     //   this.updateTodo(result);
        // }
      });
    }
  }
}
