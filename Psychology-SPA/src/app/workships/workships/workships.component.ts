import { Reception } from '../../_models/reception';
import { AuthService } from './../../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { CustomDateFormatter } from './custom-date-formatter.provider';
import { CalendarDateFormatter, CalendarEvent } from 'angular-calendar';
import { addHours, parseISO, format } from 'date-fns';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};


@Component({
  selector: 'app-workships',
  templateUrl: './workships.component.html',
  styleUrls: ['./workships.component.css'],
  providers: [
    {
      provide: CalendarDateFormatter,
      useClass: CustomDateFormatter
    }
  ]
})
export class WorkshipsComponent implements OnInit {

  viewDate: Date = new Date();
  locale = 'ru';
  receptions: Reception[];
  events: CalendarEvent[] = [];
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.getReceptions();
  }
  public logged(): boolean {
    return this.authService.loggedIn();
  }
  private getReceptions() {
    this.receptions = this.authService.receptions;
    for (const reception of this.receptions) {
      const datetime = new Date(reception.dateTimeReception);
      const startTime: string = datetime.toISOString();
      const event: CalendarEvent = {
        start: addHours(parseISO(startTime), 0),
        end: addHours(parseISO(startTime), 1),
        title: reception.fullname,
        color: colors.red
      };
      this.events.push(event);
    }
  }
}
