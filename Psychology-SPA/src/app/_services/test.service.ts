import { QuestionsAnswers } from '../_models/questionsAnswers';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Test } from '../_models/test';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  BASE_URL_DOCTOR = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }

  getTests(doctorId: number, patientId: number): Observable<Test[]> {
    return this.http.get<Test[]>(this.BASE_URL_DOCTOR + doctorId + '/patients/' + patientId + '/tests');
  }
  getTest(doctorId: number, patientId: number, testId: number): Observable<Test> {
    return this.http.get<Test>(this.BASE_URL_DOCTOR + doctorId + '/patients/' + patientId + '/tests/' + testId);
  }
  sendQuestionsAnswers(doctorId: number, patientId: number, testId: number, questionsAnswers: QuestionsAnswers) {
    return this.http.post(this.BASE_URL_DOCTOR + doctorId + '/patients/' + patientId + '/tests/' + testId, questionsAnswers);
  }
}
