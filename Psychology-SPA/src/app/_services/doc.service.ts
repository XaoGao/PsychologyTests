import { DocumentType } from './../_models/documentType';
import { environment } from './../../environments/environment';
import { Document } from './../_models/document';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DocService {

  private BASE_URL_DOC = environment.apiUrl + 'doctors/';
  constructor(private http: HttpClient) { }

  public getDocumentTypes(doctorId: number, patientId: number): Observable<DocumentType[]>  {
    return this.http.get<DocumentType[]>(this.getDocumentUrl(doctorId, patientId));
  }
  public getDocuments(doctorId: number, patientId: number): Observable<Document[]> {
    return this.http.get<Document[]>(this.getDocumentUrl(doctorId, patientId) + 'GetDocuments');
  }
  public deleteDocument(doctorId: number, patientId: number, documentId: number) {
    return this.http.delete(this.getDocumentUrl(doctorId, patientId) + documentId);
  }
  public downloadDocument(doctorId: number, patientId: number, documentId: number) {
    return this.http.get(this.getDocumentUrl(doctorId, patientId) + documentId, { responseType: 'blob' });
  }
  public interdepartReguest(documentId: number) {
    return this.http.put(environment.apiUrl + 'Interdeparts/Doc' + documentId + '/request', {});
  }
  public changeInterdepart(interdepartType: string) {
    let params = new HttpParams();
    params = params.append('interdepartType', interdepartType);

    return this.http.put(environment.apiUrl + 'Interdeparts/changeinterdepart', { params });
  }
  private getDocumentUrl(doctorId: number, patientId: number): string {
    return this.BASE_URL_DOC + doctorId + '/patients/' + patientId + '/doc/';
  }
}
