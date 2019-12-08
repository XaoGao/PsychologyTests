import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Position } from './../../_models/position';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.css']
})
export class PositionComponent implements OnInit {
  public positions: Position[];
  displayedColumns: string[] = ['position', 'name', 'sortLevel', 'isLock'];
  dataSource = new MatTableDataSource(this.positions);
  constructor(private toastrService: ToastrAlertService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.positions = data.positions;
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
