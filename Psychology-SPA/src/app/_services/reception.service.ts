import { Reception } from './../_models/reception';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReceptionService {

  private BASE_RECEPTION_URL = environment.apiUrl + 'receptions/';
  constructor(private http: HttpClient) { }

  public getFreeTime(doctorId: number, date: Date): Observable<Date[]> {
    let params = new HttpParams();

    params = params.append('DoctorId', String(doctorId));
    params = params.append('DateTimeReception', date.toLocaleDateString());

    return this.http.get<Date[]>(this.BASE_RECEPTION_URL + 'getfreetime', { observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  public createReception(reception: Reception) {
    return this.http.post(this.BASE_RECEPTION_URL, reception);
  }
}
