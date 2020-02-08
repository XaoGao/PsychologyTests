import { PatientTestResult } from './../../_models/patientTestResults';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-detail-history',
  templateUrl: './test-detail-history.component.html',
  styleUrls: ['./test-detail-history.component.css']
})
export class TestDetailHistoryComponent implements OnInit {

  patientTestResult: PatientTestResult;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.patientTestResult = data.patientTestHistory;
    });
  }
}
