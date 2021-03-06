import { Patient } from '../../_models/patient';
import { ToastrAlertService } from '../../_services/toastr-alert.service';
import { AuthService } from '../../_services/auth.service';
import { PatientService } from '../../_services/patient.service';
import { Anamnesis } from '../../_models/anamnesis';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-anamneses-list',
  templateUrl: './anamneses-list.component.html',
  styleUrls: ['./anamneses-list.component.css']
})
export class AnamnesesListComponent implements OnInit {

  public anamneses: Anamnesis[];
  public patient: Patient;
  public isNewRecord = false;
  public newRecord = new Anamnesis();
  constructor(private route: ActivatedRoute,
              private patientService: PatientService,
              private authService: AuthService,
              private toastrService: ToastrAlertService,
              private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.anamneses = data.anamneses;
      this.patient = data.patient;
    });
    this.newRecord.conclusion = '';
  }
  public addNewRecordAnamnesis(): void {
    this.isNewRecord = !this.isNewRecord;
  }
  public addAnamnesis() {
    const doctorId = this.authService.doctorId;
    const patientId = +this.route.snapshot.paramMap.get('id');

    this.newRecord.doctorId = doctorId;
    this.newRecord.patientId = patientId;

    this.patientService.createAnamnesis(doctorId, patientId, this.newRecord).subscribe(() => {
      this.toastrService.success('Запись успешно добавлена в историю');
      this.router.navigate(['/patients']);
    }, err => {
      this.toastrService.error(err);
    });
  }
}
