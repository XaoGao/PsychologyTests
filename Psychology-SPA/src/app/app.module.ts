import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
// Guard
import { ErrorInterceptor } from './_services/error.interceptor';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedTestGuard } from './_guards/prevent-unsaved-test.guard';
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
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDialogModule } from '@angular/material/dialog';
import { MAT_DATE_LOCALE } from '@angular/material';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
// Service
import { AuthService } from './_services/auth.service';
import { DoctorService } from './_services/doctor.service';
import { ToastrAlertService } from './_services/toastr-alert.service';
import { TestService } from './_services/test.service';
// Resolver
import { DoctorDetailResolver } from './_resolvers/doctor-detail.resolver';
import { DoctorsListResolver } from './_resolvers/doctors-list.resolver';
import { DepartmentsResolver } from './_resolvers/departments.resolver';
import { PhonesResolver } from './_resolvers/phones.resolver';
import { PositionsResolver } from './_resolvers/positions.resolver';
import { DepartmentsWithParamResolver } from './_resolvers/departmentsWithParam.resolver';
import { PositionsWithParamResolver } from './_resolvers/positionsWithParam.resolver';
import { PhonebookResolver } from './_resolvers/phonebook.resolver';
import { PatientsListResolver } from './_resolvers/patients-list.resolver';
import { PatientsListForRegistryResolver } from './_resolvers/patients-list-for-registry.resolver';
import { AnamnesesListResolver } from './_resolvers/anamneses-list.resolver';
import { TestsResolver } from './_resolvers/tests.resolver';
import { TestResolver } from './_resolvers/test.resolver';
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
import { PatientsListComponent } from './patients-list/patients-list.component';
import { AnamnesesListComponent } from './anamneses-list/anamneses-list.component';
import { SelectTestComponent } from './select-test/select-test.component';
import { TestComponent } from './tests/test/test.component';
import { PatientsListForRegistryComponent } from './patients-list-for-registry/patients-list-for-registry.component';
// tslint:disable-next-line:max-line-length
import { PatientForRegistryDetailComponent } from './patients-list-for-registry/patient-for-registry-detail/patient-for-registry-detail.component';
// ModalWindow
import { PatientEditComponent } from './patients-list/patient-edit/patient-edit.component';
import { ModalDepartmentComponent } from './phonebook/department/modal-department/modal-department.component';
import { ModalPositionComponent } from './phonebook/position/modal-position/modal-position.component';
import { ModalPhoneComponent } from './phonebook/phone/modal-phone/modal-phone.component';
// fullcalendar
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { PatientForRegistryComponent } from './patients-list-for-registry/patient-for-registry/patient-for-registry.component';
// Upload File
import { FileUploadModule } from 'ng2-file-upload';


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
    DoctorDetailComponent,
    PatientsListComponent,
    AnamnesesListComponent,
    PatientEditComponent,
    ModalDepartmentComponent,
    ModalPositionComponent,
    ModalPhoneComponent,
    SelectTestComponent,
    TestComponent,
    PatientsListForRegistryComponent,
    PatientForRegistryDetailComponent,
    PatientForRegistryComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FileUploadModule,
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
    MatSelectModule,
    MatTableModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatDialogModule,
    MatRadioModule,
    FullCalendarModule,
    MatCardModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory })
  ],
  providers: [
    ErrorInterceptor,
    AuthGuard,
    PreventUnsavedTestGuard,
    AuthService,
    ToastrAlertService,
    DoctorService,
    TestService,
    DoctorDetailResolver,
    DoctorsListResolver,
    DepartmentsResolver,
    PositionsResolver,
    PhonesResolver,
    PhonebookResolver,
    PatientsListResolver,
    PatientsListForRegistryResolver,
    DepartmentsWithParamResolver,
    PositionsWithParamResolver,
    AnamnesesListResolver,
    TestsResolver,
    TestResolver,
    { provide: MAT_DATE_LOCALE, useValue: 'ru-RU' }
  ],
  bootstrap: [
    AppComponent
  ],
    entryComponents: [
      PatientEditComponent,
      ModalDepartmentComponent,
      ModalPositionComponent,
      ModalPhoneComponent
    ],
})
export class AppModule { }
