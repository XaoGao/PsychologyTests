import { AuthService } from './../../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { CustomDateFormatter } from './custom-date-formatter.provider';
import { CalendarDateFormatter } from 'angular-calendar';
// import dayGridPlugin from '@fullcalendar/daygrid';
// import timeGrigPlugin from '@fullcalendar/timegrid';
// import interactionPlugin from '@fullcalendar/interaction';

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

  // calendarPlugins = [dayGridPlugin];
  viewDate: Date = new Date();
  events = [];
  locale = 'ru';
  constructor(private authService: AuthService) { }

  ngOnInit() {
    // this.calendarPlugins.setOptions('height', 700);
    // console.log(this.calendarPlugins);
  }
  logged(): boolean {
    return this.authService.loggedIn();
  }
}
