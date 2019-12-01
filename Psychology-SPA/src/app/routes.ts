import { DoctorEditComponent } from './doctor-edit/doctor-edit.component';
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

const routes: Routes = [
    {
        path: '',
        component: AuthLayoutComponent,
        children: [
            { path: '', component: HomeComponent}
        ]
    },
    {
        path: '',
        component: WorksheepsLayoutComponent,
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'phonebook', component: PhonebookComponent, resolve: { departmentsWithDoctors: PhonebookResolver }},
            { path: 'department', component: DepartmentComponent, resolve: { departments: DepartmentsResolver }},
            { path: 'position', component: PositionComponent, resolve: { positions: PositionsResolver }},
            { path: 'phone', component: PhoneComponent, resolve: { phones: PhonesResolver }},
            { path: 'workship/:id', component: WorkshipsComponent },
            { path: 'doctor/edit', component: DoctorEditComponent, resolve: { doctor: DoctorDetailResolver }},
        ]
    },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
