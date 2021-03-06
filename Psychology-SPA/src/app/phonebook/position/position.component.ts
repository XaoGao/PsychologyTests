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
  public dataSource = new MatTableDataSource(this.positions);
  constructor(private toastrService: ToastrAlertService,
              private route: ActivatedRoute,
              private phonebookService: PhonebookService,
              public dialog: MatDialog,
              private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.positions = data.positions;
    });
    this.dataSource = new MatTableDataSource(this.positions);
  }
  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  public addPosition() {
    this.openDialog();
  }
  public editPosition(position: Position) {
    this.openDialog(position);
  }
  private openDialog(currentPosition?: Position): void {
    if (!currentPosition) {
      const dialogRef = this.dialog.open(ModalPositionComponent, {
        width: '600px',
        data: { position: new Position()}
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.createPosition(result as Position);
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
  private updatePosition(position: Position) {
    this.phonebookService.updatePosition(position.id, position).subscribe((res: Position) => {
        this.toastrService.success('Данные успешно обновлены');
        this.positions.sort((pos1, pos2) => {
          if (pos1.sortLevel > pos2.sortLevel) {
            return 1;
          } else {
            return -1;
          }
        });
      }, err => {
        this.toastrService.error(err);
      }
    );
  }
  private createPosition(position: Position) {
    this.phonebookService.createPosition(this.authService.doctorId, position).subscribe((res: Position) => {
        this.toastrService.success('Новая должность успешно добавлена');
        this.positions.push(res);
        this.dataSource = new MatTableDataSource(this.positions);
        this.positions.sort((pos1, pos2) => {
          if (pos1.sortLevel > pos2.sortLevel) {
            return 1;
          } else {
            return -1;
          }
        });
      }, err => {
        this.toastrService.error(err);
      });
  }
}
