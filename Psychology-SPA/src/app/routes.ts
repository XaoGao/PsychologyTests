import { AboutComponent } from './about/about.component';
import { DoctorResolver } from './_resolvers/doctor.resolver';
import { DoctorComponent } from './doctors-list/doctor/doctor.component';
import { RolesComponent } from './roles/roles.component';
import { DocumentsListResolver } from './_resolvers/documents-list.resolver';
import { TestDetailHistoryComponent } from './tests/test-detail-history/test-detail-history.component';
import { TestHistoryComponent } from './tests/test-history/test-history.component';
import { CreateVacationComponent } from './vacations/create-vacation/create-vacation.component';
import { VacationsListResolver } from './_resolvers/vacations-list.resolver';
import { VacationsComponent } from './vacations/vacations.component';
import { ReceptionsComponent } from './receptions/receptions.component';
import { PatientResolver } from './_resolvers/patient.resolver';
import { PatientForRegistryComponent } from './patients-list-for-registry/patient-for-registry/patient-for-registry.component';
import { PatientsListForRegistryResolver } from './_resolvers/patients-list-for-registry.resolver';
import { PatientsListForRegistryComponent } from './patients-list-for-registry/patients-list-for-registry.component';
import { PreventUnsavedTestGuard } from './_guards/prevent-unsaved-test.guard';
import { TestComponent } from './tests/test/test.component';
import { TestsResolver } from './_resolvers/tests.resolver';
import { SelectTestComponent } from './select-test/select-test.component';
import { PositionsWithParamResolver } from './_resolvers/positionsWithParam.resolver';
import { PatientsListResolver } from './_resolvers/patients-list.resolver';
import { PatientsListComponent } from './patients-list/patients-list.component';
import { DoctorsListComponent } from './doctors-list/doctors-list.component';
import { PhonebookResolver } from './_resolvers/phonebook.resolver';
import { PhonesResolver } from './_resolvers/phones.resolver';
import { PositionsResolver } from './_resolvers/positions.resolver';
import { DepartmentsResolver } from './_resolvers/departments.resolver';
import { DoctorDetailResolver } from './_resolvers/doctor-detail.resolver';
import { PhoneComponent } from './phonebook/phone/phone.component';
import { PositionComponent } from './phonebook/position/position.component';
import { DepartmentComponent } from './phonebook/department/department.component';
import { PhonebookComponent } from './phonebook/phonebook.component';
import { WorkshipsComponent } from './workships/workships/workships.component';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { WorksheepsLayoutComponent } from './layouts/worksheeps-layout/worksheeps-layout.component';
import { AuthGuard } from './_guards/auth.guard';
import { DoctorDetailComponent } from './doctor-detail/doctor-detail.component';
import { DepartmentsWithParamResolver } from './_resolvers/departmentsWithParam.resolver';
import { AnamnesesListResolver } from './_resolvers/anamneses-list.resolver';
import { AnamnesesListComponent } from './anamneses-list/anamneses-list.component';
import { TestResolver } from './_resolvers/test.resolver';
import { DoctorsListResolver } from './_resolvers/doctors-list.resolver';
import { DocumentTypesResolver } from './_resolvers/document-types.resolver';
import { VacationsListForDoctorResolver } from './_resolvers/vacations-list-for-doctor.resolver';
import { PatientTestResultsListResolver } from './_resolvers/patient-test-results-list.resolver';
import { PatientTestResultsDetailResolver } from './_resolvers/patient-test-result-detail.resolver';
import { DoctorsListForAdminResolver } from './_resolvers/doctors-list-for-admin.resolver';
import { RolesListResolver } from './_resolvers/roles-list.resolver';

const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [{ path: '', component: HomeComponent }]
  },
  {
    path: '',
    component: WorksheepsLayoutComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'phonebook',
        component: PhonebookComponent,
        resolve: { departmentsWithDoctors: PhonebookResolver }
      },
      {
        path: 'department',
        component: DepartmentComponent,
        resolve: { departments: DepartmentsResolver }
      },
      {
        path: 'position',
        component: PositionComponent,
        resolve: { positions: PositionsResolver }
      },
      {
        path: 'phone',
        component: PhoneComponent,
        resolve: { phones: PhonesResolver }
      },
      { path: 'workship/:id', component: WorkshipsComponent },
      {
        path: 'doctor/edit',
        component: DoctorDetailComponent,
        resolve: {
          doctor: DoctorDetailResolver,
          departments: DepartmentsWithParamResolver,
          positions: PositionsWithParamResolver,
          phones: PhonesResolver,
          vacations: VacationsListForDoctorResolver
        }
      },
      {
        path: 'doctors',
        component: DoctorsListComponent,
        resolve: { doctors: DoctorsListForAdminResolver }
      },
      {
        path: 'doctors/:id',
        component: DoctorComponent,
        resolve: {
          doctor: DoctorResolver,
          departments: DepartmentsWithParamResolver,
          positions: PositionsWithParamResolver,
          phones: PhonesResolver,
          roles: RolesListResolver
        }
      },
      {
        path: 'roles',
        component: RolesComponent,
        resolve: { roles: RolesListResolver }
      },
      {
        path: 'patients',
        component: PatientsListComponent,
        resolve: { patients: PatientsListResolver }
      },
      {
        path: 'patientsforregistry',
        component: PatientsListForRegistryComponent,
        resolve: { patients: PatientsListForRegistryResolver }
      },
      {
        path: 'patientsforregistry/:id',
        component: PatientForRegistryComponent,
        resolve: { patient: PatientResolver, docTypes: DocumentTypesResolver,
                    doctors: DoctorsListResolver, documents: DocumentsListResolver}
      },
      {
        path: 'patients/:id/anamneses',
        component: AnamnesesListComponent,
        resolve: { patient: PatientResolver, anamneses: AnamnesesListResolver }
      },
      {
        path: 'patients/:id/tests',
        component: SelectTestComponent,
        resolve: { tests: TestsResolver }
      },
      {
        path: 'patients/:id/tests/:testId',
        component: TestComponent,
        resolve: { test: TestResolver },
        canDeactivate: [PreventUnsavedTestGuard]
      },
      {
        path: 'receptions',
        component: ReceptionsComponent,
        resolve: { doctors: DoctorsListResolver }
      },
      {
        path: 'vacations',
        component: VacationsComponent,
        resolve: { vacations: VacationsListResolver }
      },
      {
        path: 'vacations/new',
        component: CreateVacationComponent,
        resolve: { doctors: DoctorsListResolver }
      },
      {
        path: 'patients/:id/testhistory',
        component: TestHistoryComponent,
        resolve: { patientTestHistoryList: PatientTestResultsListResolver, patient: PatientResolver }
      },
      {
        path: 'patients/:id/testhistory/:testhistoryId',
        component: TestDetailHistoryComponent,
        resolve: { patientTestHistory: PatientTestResultsDetailResolver }
      },
      {
        path: 'about',
        component: AboutComponent
      }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
