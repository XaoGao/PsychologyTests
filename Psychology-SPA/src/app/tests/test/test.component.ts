import { Test } from './../../_models/test';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  public test: Test;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.test = data.test;
    });
    console.log(this.test);
  }

}
