import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ToastrAlertService } from './_services/toastr-alert.service';
import { ErrorInterceptor } from './_services/error.interceptor';

import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';

import { MatTabsModule } from '@angular/material/tabs';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    MatTabsModule
  ],
  providers: [
    ErrorInterceptor,
    ToastrAlertService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
