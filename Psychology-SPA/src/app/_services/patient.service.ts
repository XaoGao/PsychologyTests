import { Anamnesis } from './../_models/anamnesis';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from '../_models/patient';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private BASE_URL_PATIENT = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }

  public getPatients(doctorId: number): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.BASE_URL_PATIENT + doctorId + '/patients');
  }
  public getPatient(doctorId: number, patientId: number): Observable<Patient> {
    return this.http.get<Patient>(this.getPatientURL(doctorId, patientId));
  }
  public updatePatient(doctorId: number, patientId: number, patient: Patient) {
    return this.http.put(this.getPatientURL(doctorId, patientId), patient);
  }
  public createPatient(doctorId: number, patient: Patient) {
    return this.http.post(this.BASE_URL_PATIENT + doctorId + '/patients/', patient);
  }
  public getAnamnesesList(doctorId: number, patientId: number): Observable<Anamnesis[]> {
    return this.http.get<Anamnesis[]>(this.getPatientURL(doctorId, patientId) + '/anamneses');
  }
  public createAnamnesis(doctorId: number, patientId: number, anamnesis: Anamnesis) {
    return this.http.post(this.getPatientURL(doctorId, patientId) + '/anamneses', anamnesis);
  }
  public getPatientsForRegistry(doctorId: number): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.BASE_URL_PATIENT + doctorId + '/patients/patientsforregistry');
  }
  public deletePatient(doctorId: number, patientId: number) {
    return this.http.delete(this.getPatientURL(doctorId, patientId));
  }
  /*
    Get url
  */
  private getPatientURL(doctorId: number, patientId: number): string {
    return this.BASE_URL_PATIENT + doctorId + '/patients/' + patientId;
  }
}
