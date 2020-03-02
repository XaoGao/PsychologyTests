import { Patient } from './patient';
import { DocumentType } from './documentType';
export class Document {
    id: number;
    docName: string;
    series: string;
    number: string;
    dateUpload: Date;
    documentTypeId: number;
    documentType: DocumentType;
    patientId: number;
    patient: Patient;
    interdepartStatusId: number;
    interdepartRequestId: number;
}
