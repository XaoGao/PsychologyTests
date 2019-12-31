import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Test } from '../_models/test';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  BASE_URL_TESTS = environment.apiUrl + 'Tests';
  constructor(private http: HttpClient) { }

  getTests(): Observable<Test[]> {
    return this.http.get<Test[]>(this.BASE_URL_TESTS);
  }
}
