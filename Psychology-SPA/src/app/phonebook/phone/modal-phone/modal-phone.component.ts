import { Phone } from './../../../_models/phone';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-phone',
  templateUrl: './modal-phone.component.html',
  styleUrls: ['./modal-phone.component.css']
})
export class ModalPhoneComponent implements OnInit {
  public phone: Phone;
  constructor(
    public dialogRef: MatDialogRef<ModalPhoneComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    if (this.data.phone) {
      this.phone = this.data.phone;
    } else {
      this.phone = new Phone();
    }
  }

}
