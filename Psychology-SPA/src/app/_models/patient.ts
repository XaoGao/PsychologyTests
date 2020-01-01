import { Anamnesis } from './anamnesis';
import { Doctor } from './doctor';
export class Patient {
    id: number;
    personalCardNumber: string;
    lastname: string;
    firstname: string;
    middlename: string;
    fullname: string;
    dateOfBirth: Date;
    doctorId: number;
    doctor: Doctor;
    anamneses: Anamnesis[];
    // For list patient
    conclusion: string;
}
