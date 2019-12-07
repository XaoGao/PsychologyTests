import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from '../_models/patient';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  BASE_URL_PATIENT = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }
  getPatients(doctorId: number): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.BASE_URL_PATIENT + doctorId + '/patients');
  }
}
