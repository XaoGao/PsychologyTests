import { QuestionsAnswers } from '../../_models/questionsAnswers';
import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { TestService } from './../../_services/test.service';
import { Test } from './../../_models/test';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { QuestionsAnswer } from 'src/app/_models/questionsAnswer';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
  @ViewChild('testForm', { static: false }) testForm: NgForm;
  public test: Test;
  public questionsAnswers: QuestionsAnswers = new QuestionsAnswers();
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private testService: TestService,
    private authService: AuthService,
    private toastrService: ToastrAlertService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.test = data.test;
    });
    this.setQuestuinsId();
  }
  public SaveTestResult(testForm: NgForm): void {
    const doctorId = this.authService.doctorId;
    const patientId = +this.route.snapshot.paramMap.get('id');
    this.testService
      .sendQuestionsAnswers(doctorId, patientId, this.test.id, this.questionsAnswers)
      .subscribe(() => {
          this.toastrService.success('Тест успешно пройден, данные сохранены');
          testForm.reset();
          this.router.navigate(['/patients']);
        },
        err => {
          this.toastrService.error(err);
        }
      );
  }
  private setQuestuinsId(): void {
    this.questionsAnswers.questionsAnswerList = [];
    // tslint:disable-next-line:prefer-for-of
    for (let index = 0; index < this.test.questions.length; index++) {
      const questionsAnswer: QuestionsAnswer = new QuestionsAnswer();
      questionsAnswer.questionId = this.test.questions[index].id;
      questionsAnswer.sortLevel = this.test.questions[index].sortLevel;
      this.questionsAnswers.questionsAnswerList.push(questionsAnswer);
    }
  }
}
