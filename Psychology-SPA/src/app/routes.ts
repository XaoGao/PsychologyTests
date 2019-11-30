import { DepartmentsResolver } from './_resolvers/departments.resolver';
import { DoctorDetailResolver } from './_resolvers/doctor-detail.resolver';
import { PhoneComponent } from './phonebook/phone/phone.component';
import { PositionComponent } from './phonebook/position/position.component';
import { DepartmentComponent } from './phonebook/department/department.component';
import { PhonebookComponent } from './phonebook/phonebook.component';
import { DoctorDetailsComponent } from './doctor-details/doctor-details.component';
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
            { path: 'phonebook', component: PhonebookComponent },
            { path: 'department', component: DepartmentComponent, resolve: {departments: DepartmentsResolver }},
            { path: 'position', component: PositionComponent },
            { path: 'phone', component: PhoneComponent },
            { path: 'workship/:id', component: WorkshipsComponent },
            { path: 'doctor/:id', component: DoctorDetailsComponent, resolve: { doctor: DoctorDetailResolver }},
        ]
    },
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
