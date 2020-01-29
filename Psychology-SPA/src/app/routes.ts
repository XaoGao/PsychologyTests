import { ReceptionsComponent } from './receptions/receptions.component';
import { PatientForRegistryResolver } from './_resolvers/patient-for-registry.resolver';
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
          phones: PhonesResolver
        }
      },
      { path: 'doctors', component: DoctorsListComponent },
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
        resolve: { patient: PatientForRegistryResolver, docTypes: DocumentTypesResolver, doctors: DoctorsListResolver }
      },
      {
        path: 'patients/:id',
        component: AnamnesesListComponent,
        resolve: { anamneses: AnamnesesListResolver }
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
