import { Position } from './../../../_models/position';
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-position',
  templateUrl: './modal-position.component.html',
  styleUrls: ['./modal-position.component.css']
})
export class ModalPositionComponent implements OnInit {
  public position: Position;
  constructor(
    public dialogRef: MatDialogRef<ModalPositionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    if (this.data.position) {
      this.position = this.data.position;
    } else {
      this.position = new Position();
    }
  }
  public isNew(): boolean {
    if (this.position.id) {
      return false;
    } else {
      return true;
    }
  }

}
