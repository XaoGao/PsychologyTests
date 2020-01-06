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

  getTests(doctorId: string, patientId: string): Observable<Test[]> {
    return this.http.get<Test[]>(this.BASE_URL_DOCTOR + doctorId + '/patients/' + patientId + '/tests');
  }
  getTest(doctorId: string, patientId: string, testId: string): Observable<Test> {
    return this.http.get<Test>(this.BASE_URL_DOCTOR + doctorId + '/patients/' + patientId + '/tests/' + testId);
  }
}
