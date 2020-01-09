import { AuthService } from './../_services/auth.service';
import { PatientService } from './../_services/patient.service';
import { Anamnesis } from './../_models/anamnesis';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-anamneses-list',
  templateUrl: './anamneses-list.component.html',
  styleUrls: ['./anamneses-list.component.css']
})
export class AnamnesesListComponent implements OnInit {

  public anamneses: Anamnesis[];
  public patientFullname;
  public isNewRecord = false;
  public newRecord = new Anamnesis();
  constructor(private route: ActivatedRoute, private patientService: PatientService, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.anamneses = data.anamneses;
      this.getFullname();
    });
    this.newRecord.conclusion = '';
  }
  private getFullname() {
    if (this.anamneses[0]) {
      this.patientFullname = this.anamneses[0].patient.fullname;
    } else {
      this.patientFullname = 'нет ни одной записи';
    }
  }
  public addNewRecordAnamnesis(): void {
    this.isNewRecord = !this.isNewRecord;
  }
  public addAnamnesis() {
    const doctorId = this.authService.decodedToken.nameid;
    const patientId = +this.route.snapshot.paramMap.get('id');
    this.newRecord.doctorId = doctorId;
    this.newRecord.patinetId = patientId;
    this.patientService.createAnamnesis(doctorId, patientId, this.newRecord);
  }
}
