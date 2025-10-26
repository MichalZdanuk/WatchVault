import { Routes } from '@angular/router';
import { RegisterPage } from './pages/register/register.page';
import { LoginPage } from './pages/login/login.page';

export const AUTH_ROUTES: Routes = [
  { path: 'login', component: LoginPage },
  { path: 'register', component: RegisterPage },
];
