import { Test } from './../_models/test';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { TestService } from './../_services/test.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-select-test',
  templateUrl: './select-test.component.html',
  styleUrls: ['./select-test.component.css']
})
export class SelectTestComponent implements OnInit {

  public tests: Test[];
  constructor(private testService: TestService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.tests = data.tests;
    });
    console.log(this.route.params);
  }
}
