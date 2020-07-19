import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';
import { RegisterComponent } from './register/register.component';
import { GridComponent } from './grid/grid.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: '', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'viewdata', component: GridComponent },

    // otherwise redirect to home
    { path: '*', redirectTo: '/login' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
