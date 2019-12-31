import { Question } from './question';
export interface Answer {
    id: number;
    text: string;
    value: number;
    question: Question;
}
