import { Routes } from '@angular/router';
import { AuthLayout } from './core/layouts/auth-layout/auth-layout';
import { LoginPage } from './features/auth/pages/login/login.page';
import { RegisterPage } from './features/auth/pages/register/register.page';
import { MainLayout } from './core/layouts/main-layout/main-layout';
import { MovieSearchPage } from './features/movies/pages/movie-search/movie-search.page';
import { WatchlistPage } from './features/watchlist/pages/watchlist/watchlist.page';
import { MovieDetailsPage } from './features/movies/pages/movie-details/movie-details.page';
import { ProfilePage } from './features/profile/pages/profile/profile.page';
import { TrendingMoviesPage } from './features/movies/pages/trending-movies/trending-movies.page';
import { HomePage } from './features/home/pages/home/home.page';
import { AuthGuard } from './core/auth/auth.guard';
import { AnalyticsPage } from './features/analytics/pages/analytics/analytics.page';
import { HistoryPage } from './features/history/pages/history/history.page';

export const routes: Routes = [
  {
    path: '',
    component: AuthLayout,
    children: [
      { path: '', component: HomePage },
      { path: 'login', component: LoginPage },
      { path: 'register', component: RegisterPage },
    ],
  },
  {
    path: '',
    component: MainLayout,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'movies/search', pathMatch: 'full' },
      { path: 'movies/search', component: MovieSearchPage },
      { path: 'movies/trending', component: TrendingMoviesPage },
      { path: 'movies/:id', component: MovieDetailsPage },
      { path: 'watchlist', component: WatchlistPage },
      { path: 'profile', component: ProfilePage },
      { path: 'analytics', component: AnalyticsPage },
      { path: 'history', component: HistoryPage },
    ],
  },

  { path: '**', redirectTo: '' },
];
