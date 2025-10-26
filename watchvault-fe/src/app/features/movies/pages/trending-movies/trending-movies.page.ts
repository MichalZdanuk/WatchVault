import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../../../core/services/movie.service';
import { TrendingMovie } from '../../../../shared/models/trending-movie.model';
import { TrendingInterval } from '../../../../shared/models/trending-interval.enum';
import { CommonModule } from '@angular/common';
import { TrendingMovieCard } from '../../trending-movie-card/trending-movie-card';
import { FlexModule } from '@angular/flex-layout';
import { MatTooltipModule } from '@angular/material/tooltip';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-trending-movies.page',
  imports: [
    MatTooltipModule,
    MatSelectModule,
    CommonModule,
    FlexModule,
    TrendingMovieCard,
    InfoIcon,
    LoadingSpinner,
    ErrorMessage,
  ],
  templateUrl: './trending-movies.page.html',
  styleUrl: './trending-movies.page.css',
})
export class TrendingMoviesPage implements OnInit {
  trendingMovies: TrendingMovie[] = [];
  filteredTrendingMovies: TrendingMovie[] = [];
  isLoading: boolean = true;
  error: string | null = null;
  sortOrder: string = 'popularity';

  trendingInterval: TrendingInterval = TrendingInterval.Today;

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.reloadTrendingMovies();
  }

  reloadTrendingMovies(): void {
    this.isLoading = true;
    this.error = null;

    this.movieService.getTrendingMovies(this.trendingInterval).subscribe({
      next: (t) => {
        this.trendingMovies = t;
        this.applySorting();
        this.isLoading = false;
      },
      error: (err) => {
        this.error = '⚠️ Failed to load trending movies. Please try again.';
        this.isLoading = false;
        console.error(err);
      },
    });
  }

  onIntervalChange(interval: TrendingInterval): void {
    this.trendingInterval = interval;
    this.reloadTrendingMovies();
  }

  onSortChange(sortValue: string): void {
    this.sortOrder = sortValue;
    this.applySorting();
  }

  private applySorting(): void {
    this.filteredTrendingMovies = [...this.trendingMovies];

    switch (this.sortOrder) {
      case 'title_asc':
        this.filteredTrendingMovies.sort((a, b) => a.title.localeCompare(b.title));
        break;
      case 'title_desc':
        this.filteredTrendingMovies.sort((a, b) => b.title.localeCompare(a.title));
        break;
      case 'release_date_asc':
        this.filteredTrendingMovies.sort(
          (a, b) => new Date(a.releaseDate).getTime() - new Date(b.releaseDate).getTime()
        );
        break;
      case 'release_date_desc':
        this.filteredTrendingMovies.sort(
          (a, b) => new Date(b.releaseDate).getTime() - new Date(a.releaseDate).getTime()
        );
        break;
      case 'imdb_rating_asc':
        this.filteredTrendingMovies.sort((a, b) => (a.imdbRating ?? 0) - (b.imdbRating ?? 0));
        break;
      case 'imdb_rating_desc':
        this.filteredTrendingMovies.sort((a, b) => (b.imdbRating ?? 0) - (a.imdbRating ?? 0));
        break;
      case 'runtime_minutes_asc':
        this.filteredTrendingMovies.sort((a, b) => a.runtimeMinutes - b.runtimeMinutes);
        break;
      case 'runtime_minutes_desc':
        this.filteredTrendingMovies.sort((a, b) => b.runtimeMinutes - a.runtimeMinutes);
        break;
      default:
        break;
    }
  }
}
