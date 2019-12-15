import { Doctor } from './doctor';
import { Patient } from './patient';

export interface Anamnesis {
    id: number;
    conclusionTime: Date;
    patinetId: number;
    patient: Patient;
    conclusion: string;
    doctorId: number;
    doctor: Doctor;
    isLast: boolean;
}
