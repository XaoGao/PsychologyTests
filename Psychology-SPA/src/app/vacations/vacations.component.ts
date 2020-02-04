import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Vacation } from '../_models/vacation';

@Component({
  selector: 'app-vacations',
  templateUrl: './vacations.component.html',
  styleUrls: ['./vacations.component.css']
})
export class VacationsComponent implements OnInit {

  public vacations: Vacation[];
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.vacations = data.vacations;
    });
  }
}
