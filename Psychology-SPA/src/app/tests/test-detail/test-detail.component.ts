import { Test } from './../../_models/test';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {

  public test: Test;
  constructor(private route: ActivatedRoute, private location: Location) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.test = data.test;
    });
  }
  public back() {
    this.location.back();
  }
}
