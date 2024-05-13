import { Routes } from '@angular/router';
import { LandingComponent } from './modules/landing/landing.component';
import { LoginComponent } from './modules/login/login.component';
import { DemoPageOneComponent } from './modules/taxCalculator/demo-page-one/demo.component';
import { DemoPageTwoComponent } from './modules/taxCalculator/demo-page-two/demo-page-two.component';

export const routes: Routes = [
    {path: '', component: LandingComponent},
    {path: 'login', component: LoginComponent},
    {path: 'demo/one', component: DemoPageOneComponent},
    {path: 'demo/two/:employeeId', component: DemoPageTwoComponent},
];
