import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from '../_models/doctor';
import { Role } from '../_models/role';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private BASE_ADMIN_URL = environment.apiUrl + 'admins/';
  constructor(private http: HttpClient) { }

  public getDoctors(adminId: number): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.getAdminUrl(adminId) + 'doctors');
  }
  public getDoctor(adminId: number, doctorId: number): Observable<Doctor> {
    return this.http.get<Doctor>(this.getAdminUrl(adminId) + 'doctors/' + doctorId);
  }
  public getRoles(adminId: number): Observable<Role[]> {
    return this.http.get<Role[]>(this.getAdminUrl(adminId) + 'roles');
  }
  public createDoctor(adminId: number, doctor: Doctor) {
    return this.http.post(this.getAdminUrl(adminId) + 'doctors', doctor);
  }
  public updateDoctor(adminId: number, doctor: Doctor) {
    return this.http.put(this.getAdminUrl(adminId) + 'doctors/' + doctor.id, doctor);
  }
  public deleteDoctor(adminId: number, doctorId: number) {
    return this.http.delete(this.getAdminUrl(adminId) + 'doctors/' + doctorId);
  }
  public dropPassword(adminId: number, doctorId: number) {
    return this.http.put(this.getAdminUrl(adminId) + 'doctors/' + doctorId + '/dropPassword', {});
  }
  private getAdminUrl(adminId: number): string {
    return this.BASE_ADMIN_URL + adminId + '/';
  }
}
