import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Doctor } from '../_models/doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  private BASE_URL_DOCTOR = environment.apiUrl + 'doctors';
  constructor(private http: HttpClient) { }

  public getDoctor(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(this.BASE_URL_DOCTOR + '/' + id);
  }
  public updateDoctor(doctor: Doctor) {
    return this.http.put(this.BASE_URL_DOCTOR + '/' + doctor.id, doctor);
  }
  public getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.BASE_URL_DOCTOR);
  }
  public createDoctor(doctor: Doctor) {
    return this.http.post(this.BASE_URL_DOCTOR, doctor);
  }
}
