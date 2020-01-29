import { DocumentType } from './../_models/documentType';
import { environment } from './../../environments/environment';
import { Document } from './../_models/document';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DocService {

  private BASE_URL_DOC = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }

  public getDocumentTypes(doctorId: number, patientId: number): Observable<DocumentType[]>  {
    return this.http.get<DocumentType[]>(this.BASE_URL_DOC + doctorId + '/patients/' + patientId + '/doc');
  }
  public deleteDocument(doctorId: number, patientId: number, documentId: number) {
    return this.http.delete(this.BASE_URL_DOC + doctorId + '/patients/' + patientId + '/doc/' + documentId);
  }
}
