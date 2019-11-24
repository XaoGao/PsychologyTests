import { DoctorDetailsComponent } from './doctor-details/doctor-details.component';
import { WorkshipsComponent } from './workships/workships/workships.component';
import { DoctorDetailResolver } from './_resolvers/doctor-detail.resolver';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'workship/:id', component: WorkshipsComponent },
            { path: 'doctor/:id', component: DoctorDetailsComponent, resolve: { doctor: DoctorDetailResolver }}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
