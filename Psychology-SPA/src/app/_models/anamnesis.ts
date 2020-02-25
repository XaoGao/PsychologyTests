import { Doctor } from './doctor';
import { Patient } from './patient';

export class Anamnesis {
    id: number;
    conclusionTime: Date;
    patinetId: number;
    patientFullname: string;
    conclusion: string;
    doctorId: number;
    doctorFullname: string;
    isLast: boolean;
}
