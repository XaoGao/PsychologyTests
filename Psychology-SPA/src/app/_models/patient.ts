import { Doctor } from './doctor';
export interface Patient {
    id: number;
    personalCardNumber: string;
    lastname: string;
    firstname: string;
    middlename: string;
    fullname: string;
    doctorId: number;
    doctor: Doctor;
}
