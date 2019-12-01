import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhonebookService {

  BASE_URL = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }

  getDepartments(doctorId: number) {
    return this.http.get(this.BASE_URL + doctorId + '/departments');
  }
  getPositions(doctorId: number) {
    return this.http.get(this.BASE_URL + doctorId + '/positions');
  }
  getPhones(doctorId: number) {
    return this.http.get(this.BASE_URL + doctorId + '/phones');
  }
  getPhonebook(doctorId: number) {
    return this.http.get(this.BASE_URL + doctorId + '/phonebook');
  }
}
