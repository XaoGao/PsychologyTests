import { Position } from './position';
import { Phone } from './phone';
import { Department } from './department';

export interface Doctor {
    id: number;
    username: string;
    lastName: string;
    firstName: string;
    middleName: string;
    fullName: string;
    dateOfBirth: Date;
    departmentId: number;
    department: Department;
    positionId: number;
    position: Position;
    phoneId: number;
    phone: Phone;
}
