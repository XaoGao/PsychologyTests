import { WorkshipsComponent } from './workships/workships/workships.component';
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
            { path: 'workship/:id', component: WorkshipsComponent }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
