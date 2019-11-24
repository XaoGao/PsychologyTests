import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Doctor } from '../_models/doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  BASE_URL_DOCTOR = environment.apiUrl + 'doctors';
  constructor(private http: HttpClient) { }

  getDoctor(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(this.BASE_URL_DOCTOR);
  }
}
