import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RolesService {

  private admin = 'admin';
  private HR = 'hr';
  private doctor = 'doctor';
  private registry = 'registry';
  constructor() { }

  // public isAdmin(role: string): boolean {
  //   if (role === this.admin) {
  //     return true;
  //   } else {
  //     return false;
  //   }
  // }
  // public isHR(role: string): boolean {
  //   if (role === this.HR) {
  //     return true;
  //   } else {
  //     return false;
  //   }
  // }
  // public isDoctor(role: string): boolean {
  //   if (role === this.doctor) {
  //     return true;
  //   } else {
  //     return false;
  //   }
  // }
  // public isRegistry(role: string): boolean {
  //   if (role === this.registry) {
  //     return true;
  //   } else {
  //     return false;
  //   }
  // }
}
