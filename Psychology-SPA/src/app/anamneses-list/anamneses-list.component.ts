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
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.anamneses = data.anamneses;
      this.getFullname();
    });
  }
  private getFullname() {
    if (this.anamneses[0]) {
      this.patientFullname = this.anamneses[0].patient.fullname;
    } else {
      this.patientFullname = 'нет ни одной записи';
    }
  }
}
