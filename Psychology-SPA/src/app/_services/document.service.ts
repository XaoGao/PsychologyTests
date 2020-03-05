import { DocumentType } from '../_models/documentType';
import { environment } from '../../environments/environment';
import { Document } from '../_models/document';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private BASE_URL_DOC = environment.apiUrl + 'doctors/';

  public realInterdepartType = true;

  constructor(private http: HttpClient) { }

  public getDocumentTypes(doctorId: number, patientId: number): Observable<DocumentType[]>  {
    return this.http.get<DocumentType[]>(this.getDocumentUrl(doctorId, patientId));
  }
  public getDocuments(doctorId: number, patientId: number): Observable<Document[]> {
    return this.http.get<Document[]>(this.getDocumentUrl(doctorId, patientId) + 'getDocuments');
  }
  public deleteDocument(doctorId: number, patientId: number, documentId: number) {
    return this.http.delete(this.getDocumentUrl(doctorId, patientId) + documentId);
  }
  public downloadDocument(doctorId: number, patientId: number, documentId: number) {
    return this.http.get(this.getDocumentUrl(doctorId, patientId) + documentId, { responseType: 'blob' });
  }
  public interdepartReguest(documentId: number) {
    return this.http.put(environment.apiUrl + 'interdeparts/document/' + documentId + '/request', {});
  }
  public changeInterdepart(interdepartType: string) {
    let params = new HttpParams();
    params = params.append('interdepartType', interdepartType);

    return this.http.put(environment.apiUrl + 'Interdeparts/changeinterdepart', {}, { params });
  }
  private getDocumentUrl(doctorId: number, patientId: number): string {
    return this.BASE_URL_DOC + doctorId + '/patients/' + patientId + '/documents/';
  }
  public interdepartStatusName(interdepartStatusId: number): string {
    switch (interdepartStatusId) {
      case 0:
        return '-';
      case 1:
        return 'Ожидает отправки';
      case 2:
        return 'Запрос отправлен';
      case 4:
        return 'Подтверждено';
      case 5:
        return 'Отказано';
      default:
        return 'Некорректный статус';
    }
  }
}
