import { Patient } from './patient';
import { Position } from './position';
import { Phone } from './phone';
import { Department } from './department';

export interface Doctor {
    id: number;
    username: string;
    password: string;
    lastname: string;
    firstname: string;
    middlename: string;
    fullname: string;
    dateOfBirth: Date;
    departmentId: number;
    department: Department;
    positionId: number;
    position: Position;
    phoneId: number;
    phone: Phone;
    patients: Patient[];
    isLock: boolean;
    roleId: number;
}
