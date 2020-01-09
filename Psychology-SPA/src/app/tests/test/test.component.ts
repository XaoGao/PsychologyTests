import { Test } from './../../_models/test';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
  @ViewChild('testForm', {static: false}) testForm: NgForm;
  public test: Test;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.test = data.test;
    });
    console.log(this.test);
  }
  public SaveTestResult(): void {
    console.log('test');
  }
}
