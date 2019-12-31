import { Answer } from './answer';
import { Question } from './question';

export interface Test {
    id: number;
    name: string;
    description: string;
    instruction: string;
    questions: Question[];
    answers: Answer[];
}
