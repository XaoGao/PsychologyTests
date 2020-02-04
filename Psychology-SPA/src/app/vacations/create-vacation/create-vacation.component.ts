import { ToastrAlertService } from './../../_services/toastr-alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Doctor } from './../../_models/doctor';
import { VacationService } from './../../_services/vacation.service';
import { Vacation } from './../../_models/vacation';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-vacation',
  templateUrl: './create-vacation.component.html',
  styleUrls: ['./create-vacation.component.css']
})
export class CreateVacationComponent implements OnInit {
  public newVacation: Vacation;
  public doctors: Doctor[];
  constructor(private vacationService: VacationService,
              private route: ActivatedRoute,
              private router: Router,
              private toastrService: ToastrAlertService) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.doctors = data.doctors;
    });
    this.newVacation = new Vacation();
  }
  public createVacation() {
    this.vacationService.createVacation(this.newVacation.doctorId, this.newVacation).subscribe(() => {
      this.toastrService.success('Вы добавили новую запись.');
      this.router.navigate(['/vacations']);
    }, err => {
      this.toastrService.error(err);
    });
  }
}
