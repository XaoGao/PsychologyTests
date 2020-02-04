import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Vacation } from '../_models/vacation';

@Injectable({
  providedIn: 'root'
})
export class VacationService {
  private BASE_URL_VACATION = environment.apiUrl + 'vacations/';
  constructor(private http: HttpClient) { }
  public getVacations() {
    return this.http.get<Vacation[]>(this.BASE_URL_VACATION);
  }
  public getVacationsForDoctor(doctorId: number) {
    return this.http.get<Vacation[]>(this.BASE_URL_VACATION + 'doctors/' + doctorId);
  }
  public createVacation(doctorId: number, vacation: Vacation) {
    return this.http.post(this.BASE_URL_VACATION + 'doctors/' + doctorId, vacation);
  }
}
