import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhonebookService {

  BASE_URL = environment.apiUrl;
  // BASE_URL = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getDepartments(doctorId: number) {
    // return this.http.get(this.BASE_URL + doctorId + '/departments');
    return this.http.get(this.BASE_URL + 'departments');
  }
  getPositions(doctorId: number) {
    return this.http.get(this.BASE_URL + 'positions');
    // return this.http.get(this.BASE_URL + doctorId + '/positions');
  }
  getPhones(doctorId: number) {
    return this.http.get(this.BASE_URL + 'phones');
    // return this.http.get(this.BASE_URL + doctorId + '/phones');
  }
  getPhonebook(doctorId: number) {
    return this.http.get(this.BASE_URL  + 'doctors/' + doctorId + '/phonebook');
  }
}
