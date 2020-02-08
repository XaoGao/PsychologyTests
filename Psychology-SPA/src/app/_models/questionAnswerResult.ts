import { Question } from './question';
import { Test } from './test';
import { Patient } from './patient';
export class QuestionAnserResult {
    id: number;
    patientId: number;
    patient: Patient;
    testId: number;
    test: Test;
    questionId: number;
    question: Question;
    answersValue: number;
}