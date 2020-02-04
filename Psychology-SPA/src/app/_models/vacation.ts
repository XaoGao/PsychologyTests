import { Doctor } from './doctor';
export class Vacation {
    id: number;
    doctorId: number;
    doctor: Doctor;
    startVacation: Date;
    endVacation: Date;
    countDays: number;
}