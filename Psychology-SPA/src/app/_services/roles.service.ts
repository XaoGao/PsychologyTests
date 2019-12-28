import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RolesService {

  public admin = 'admin';
  public HR = 'hr';
  public doctor = 'doctor';
  constructor() { }
}
