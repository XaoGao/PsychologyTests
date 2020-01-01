import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from '../_models/patient';
import { Anamnesis } from '../_models/anamnesis';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  BASE_URL_PATIENT = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }

  getPatients(doctorId: number): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.BASE_URL_PATIENT + doctorId + '/patients');
  }

  getAnamnesesList(doctorId: number, patientId: number): Observable<Anamnesis[]> {
    return this.http.get<Anamnesis[]>(this.BASE_URL_PATIENT + doctorId + '/patients/' + patientId + '/anamnses');
  }
  updatePatient(doctorId: number, patientId: number, patient: Patient) {
    return this.http.put(this.BASE_URL_PATIENT + doctorId + '/patients/' + patientId, patient);
  }
  createPatient(doctorId: number, patient: Patient) {
    return this.http.post(this.BASE_URL_PATIENT + doctorId + '/patients/', patient);
  }
}
