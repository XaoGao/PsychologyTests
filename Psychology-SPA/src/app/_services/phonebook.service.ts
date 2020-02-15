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

  private BASE_URL = environment.apiUrl;
  constructor(private http: HttpClient) { }

  public getDepartments(param: boolean): Observable<Department[]> {
    let params = new HttpParams();
    params = params.append('param', String(param));
    return this.http.get<Department[]>(this.BASE_URL + 'departments', { observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }
  public getPositions(param: boolean): Observable<Position[]> {
    let params = new HttpParams();
    params = params.append('param', String(param));
    return this.http.get<Position[]>(this.BASE_URL + 'positions', { observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }
  public getPhones(): Observable<Phone[]> {
    return this.http.get<Phone[]>(this.BASE_URL + 'phones');
  }
  public getPhonebook(doctorId: number) {
    return this.http.get(this.BASE_URL  + 'doctors/' + doctorId + '/phonebook');
  }
  public updateDepartment(departmentId: number, department: Department) {
    return this.http.put(this.BASE_URL + 'departments/' + departmentId, department);
  }
  public createDepartment(doctorId: number, department: Department) {
    return this.http.post(this.BASE_URL + 'departments/' + doctorId, department);
  }

  public updatePosition(positionId: number, position: Position) {
    return this.http.put(this.BASE_URL + 'positions/' + positionId, position);
  }
  public createPosition(doctorId: number, position: Position) {
    return this.http.post(this.BASE_URL + 'positions/' + doctorId, position);
  }

  public updatePhone(phoneId: number, phone: Phone) {
    return this.http.put(this.BASE_URL + 'phones/' + phoneId, phone);
  }
  public createPhone(doctorId: number, phone: Phone) {
    return this.http.post(this.BASE_URL + 'phones/' + doctorId, phone);
  }
  public  deleteDepartment(doctorId: number, departmentId: number) {
    let params = new HttpParams();
    params = params.append('doctorId', String(doctorId));
    return this.http.delete(this.BASE_URL + 'department/' + departmentId, { observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }
  public deletePosition(doctorId: number, positionId: number) {
    let params = new HttpParams();
    params = params.append('doctorId', String(doctorId));
    return this.http.delete(this.BASE_URL + 'Position/' + positionId, { observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }
  public deletePhone(doctorId: number, phoneId: number) {
    let params = new HttpParams();
    params = params.append('doctorId', String(doctorId));
    return this.http.delete(this.BASE_URL + 'Phone/' + phoneId, { observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }
}
