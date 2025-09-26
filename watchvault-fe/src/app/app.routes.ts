import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './core/layouts/auth-layout.component/auth-layout.component';
import { LoginComponent } from './features/auth/pages/login.component/login.component';
import { RegisterComponent } from './features/auth/pages/register.component/register.component';
import { MainLayoutComponent } from './core/layouts/main-layout.component/main-layout.component';
import { MovieSearchComponent } from './features/movies/pages/movie-search.component/movie-search.component';
import { WatchlistComponent } from './features/watchlist/pages/watchlist.component/watchlist.component';
import { MovieDetailsComponent } from './features/movies/pages/movie-details.component/movie-details.component';
import { ProfileComponent } from './features/profile/pages/profile.component/profile.component';
import { TrendingMoviesComponent } from './features/movies/pages/trending-movies.component/trending-movies.component';
import { HomeComponent } from './features/home/pages/home.component/home.component';

export const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
    ],
  },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: 'movies/search', pathMatch: 'full' },
      { path: 'movies/search', component: MovieSearchComponent },
      { path: 'movies/trending', component: TrendingMoviesComponent },
      { path: 'movies/:id', component: MovieDetailsComponent },
      { path: 'watchlist', component: WatchlistComponent },
      { path: 'profile', component: ProfileComponent },
    ],
  },

  { path: '**', redirectTo: '' },
];
