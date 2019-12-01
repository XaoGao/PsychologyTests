import { Doctor } from './doctor';
import { Department } from './department';

export interface DepartmentWithDoctors {
    department: Department;
    doctors: Doctor[];
}
