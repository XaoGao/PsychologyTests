import { Department } from './../_models/department';
import { map } from 'rxjs/operators';
import { Phone } from './../_models/phone';
import { Position } from './../_models/position';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhonebookService {

  BASE_URL = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getDepartments(param: boolean): Observable<Department[]> {
    let params = new HttpParams();
    params = params.append('param', String(param));
    return this.http.get<Department[]>(this.BASE_URL + 'departments', { observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }
  getPositions(param: boolean): Observable<Position[]> {
    let params = new HttpParams();
    params = params.append('param', String(param));
    return this.http.get<Position[]>(this.BASE_URL + 'positions', { observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }
  getPhones(): Observable<Phone[]> {
    return this.http.get<Phone[]>(this.BASE_URL + 'phones');
  }
  getPhonebook(doctorId: number) {
    return this.http.get(this.BASE_URL  + 'doctors/' + doctorId + '/phonebook');
  }
  updateDepartment(departmentId: number, department: Department) {
    return this.http.put(this.BASE_URL + 'departments/' + departmentId, department);
  }
}
