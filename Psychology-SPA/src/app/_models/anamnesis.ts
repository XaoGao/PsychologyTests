import { Doctor } from './doctor';
import { Patient } from './patient';

export class Anamnesis {
    id: number;
    conclusionTime: Date;
    patientId: number;
    patientFullname: string;
    conclusion: string;
    doctorId: number;
    doctorFullname: string;
    isLast: boolean;
}
