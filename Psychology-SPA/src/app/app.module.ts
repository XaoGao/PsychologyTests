import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
// Guard
import { ErrorInterceptor } from './_services/error.interceptor';
import { AuthGuard } from './_guards/auth.guard';
// Routing
import { JwtModule } from '@auth0/angular-jwt';
import { AppRoutingModule } from './routes';
// MatModule
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material';
import { MatSelectModule } from '@angular/material/select';
// Service
import { AuthService } from './_services/auth.service';
import { DoctorService } from './_services/doctor.service';
import { ToastrAlertService } from './_services/toastr-alert.service';
// Resolver
import { DoctorDetailResolver } from './_resolvers/doctor-detail.resolver';
import { DepartmentsResolver } from './_resolvers/departments.resolver';
import { PhonesResolver } from './_resolvers/phones.resolver';
import { PositionsResolver } from './_resolvers/positions.resolver';
import { PhonebookResolver } from './_resolvers/phonebook.resolver';
// Component
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { WorksheepsLayoutComponent } from './layouts/worksheeps-layout/worksheeps-layout.component';
import { DepartmentComponent } from './phonebook/department/department.component';
import { PositionComponent } from './phonebook/position/position.component';
import { PhoneComponent } from './phonebook/phone/phone.component';
import { DoctorsListComponent } from './doctors-list/doctors-list.component';
import { DoctorDetailComponent } from './doctor-detail/doctor-detail.component';
import { WorkshipsComponent } from './workships/workships/workships.component';
import { SidebarComponent } from './sidebar/sidebar/sidebar.component';
import { PhonebookComponent } from './phonebook/phonebook.component';


export function tokenGetter() {
  return localStorage.getItem('token');
}


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    WorkshipsComponent,
    SidebarComponent,
    PhonebookComponent,
    AuthLayoutComponent,
    WorksheepsLayoutComponent,
    DepartmentComponent,
    PositionComponent,
    PhoneComponent,
    DoctorsListComponent,
    DoctorDetailComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    AppRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
    MatTabsModule,
    MatInputModule,
    MatMenuModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule
  ],
  providers: [
    ErrorInterceptor,
    AuthGuard,
    AuthService,
    ToastrAlertService,
    DoctorService,
    DoctorDetailResolver,
    DepartmentsResolver,
    PositionsResolver,
    PhonesResolver,
    PhonebookResolver
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
