import { Patient } from './patient';
import { DocumentType } from './documentType';
export class Document {
    id: number;
    docName: string;
    series: string;
    number: string;
    dateUpload: Date;
    documentTypeId: number;
    documenType: DocumentType;
    patientId: number;
    patient: Patient;
}
