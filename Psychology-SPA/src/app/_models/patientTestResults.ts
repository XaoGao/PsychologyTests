import { QuestionAnserResult } from './questionAnswerResult';
import { ProcessingInterpretationOfResult } from './processingInterpretationOfResult';
import { Test } from './test';
import { Doctor } from './doctor';
import { Patient } from './patient';
export interface PatientTestResult {
    id: number;
    patientId: number;
    patient: Patient;
    doctorId: number;
    doctor: Doctor;
    testId: number;
    test: Test;
    testResultInPoints: number;
    processingInterpretationOfResultId: number;
    processingInterpretationOfResult: ProcessingInterpretationOfResult;
    dateTimeCreate: Date;
    questionsAnswers: QuestionAnserResult[];
}
