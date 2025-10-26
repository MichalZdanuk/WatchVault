import { Routes } from '@angular/router';
import { AuthLayout } from './core/layouts/auth-layout/auth-layout';
import { MainLayout } from './core/layouts/main-layout/main-layout';
import { AuthGuard } from './core/auth/auth.guard';
import { MOVIES_ROUTES } from './features/movies/movies.routes';
import { PROFILE_ROUTES } from './features/profile/profile.routes';
import { WATCHLIST_ROUTES } from './features/watchlist/watchlist.routes';
import { HOME_ROUTES } from './features/home/home.routes';
import { HISTORY_ROUTES } from './features/history/history.routes';
import { ANALYTICS_ROUTES } from './features/analytics/analytics.routes';
import { AUTH_ROUTES } from './features/auth/auth.routes';

export const routes: Routes = [
  {
    path: '',
    component: AuthLayout,
    children: [...HOME_ROUTES, ...AUTH_ROUTES],
  },
  {
    path: '',
    component: MainLayout,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'movies/search', pathMatch: 'full' },
      ...MOVIES_ROUTES,
      ...WATCHLIST_ROUTES,
      ...PROFILE_ROUTES,
      ...ANALYTICS_ROUTES,
      ...HISTORY_ROUTES,
    ],
  },

  { path: '**', redirectTo: '' },
];
