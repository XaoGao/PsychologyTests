import { TestService } from './../../_services/test.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from './../../_services/auth.service';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { Component, OnInit } from '@angular/core';
import { PatientTestResult } from 'src/app/_models/patientTestResults';

@Component({
  selector: 'app-test-history',
  templateUrl: './test-history.component.html',
  styleUrls: ['./test-history.component.css']
})
export class TestHistoryComponent implements OnInit {

  patientTestHistoryList: PatientTestResult[];
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private testService: TestService,
    private authService: AuthService,
    private toastrService: ToastrAlertService
  ) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.patientTestHistoryList = data.patientTestHistoryList;
    });
  }

}
