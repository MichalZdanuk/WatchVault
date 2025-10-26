import { Routes } from '@angular/router';
import { MovieSearchPage } from './pages/movie-search/movie-search.page';
import { TrendingMoviesPage } from './pages/trending-movies/trending-movies.page';
import { MovieDetailsPage } from './pages/movie-details/movie-details.page';

export const MOVIES_ROUTES: Routes = [
  { path: 'movies/search', component: MovieSearchPage },
  { path: 'movies/trending', component: TrendingMoviesPage },
  { path: 'movies/:id', component: MovieDetailsPage },
];
