import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Phone } from './../../_models/phone';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-phone',
  templateUrl: './phone.component.html',
  styleUrls: ['./phone.component.css']
})
export class PhoneComponent implements OnInit {
  public phones: Phone[];
  displayedColumns: string[] = ['position', 'numberMask', 'isLock'];
  dataSource = new MatTableDataSource(this.phones);
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.phones = data.phones;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
