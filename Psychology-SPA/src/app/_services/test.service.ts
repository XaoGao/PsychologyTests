import { QuestionsAnswers } from '../_models/questionsAnswers';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Test } from '../_models/test';
import { PatientTestResult } from '../_models/patientTestResults';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private BASE_URL_DOCTOR = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }

  public getTests(doctorId: number, patientId: number): Observable<Test[]> {
    return this.http.get<Test[]>(this.getTestsURL(doctorId, patientId));
  }
  public getTest(doctorId: number, patientId: number, testId: number): Observable<Test> {
    return this.http.get<Test>(this.getTestsURL(doctorId, patientId) + testId);
  }
  public sendQuestionsAnswers(doctorId: number, patientId: number, testId: number, questionsAnswers: QuestionsAnswers) {
    return this.http.post(this.getTestsURL(doctorId, patientId) + testId, questionsAnswers);
  }
  public getTestsHistory(doctorId: number, patientId: number): Observable<PatientTestResult[]> {
    return this.http.get<PatientTestResult[]>(this.getTestsURL(doctorId, patientId) + 'gethistory');
  }
  public getTestHistory(doctorId: number, patientId: number, patientTestHistoryId: number): Observable<PatientTestResult> {
    return this.http.get<PatientTestResult>(this.getTestsURL(doctorId, patientId) + 'gethistory/' + patientTestHistoryId);
  }
  /**
   * URL
   */
  private getTestsURL(doctorId: number, patientId: number): string {
    return this.BASE_URL_DOCTOR + doctorId + '/patients/' + patientId + '/tests/';
  }
}
