import { Test } from './test';
import { Answer } from './answer';

export interface Question {
    id: number;
    text: string;
    sortLevel: number;
    testId: number;
    test: Test;
    answers: Answer[];
}
