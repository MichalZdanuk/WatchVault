import { Component, OnInit } from '@angular/core';
import { SimklService } from '../../../../core/services/simkl.service';
import { TrendingMovie } from '../../../../shared/models/trending-movie';
import { TrendingInterval } from '../../../../shared/models/trending-interval';
import { CommonModule } from '@angular/common';
import { TrendingMovieCard } from '../../trending-movie-card/trending-movie-card';
import { FlexModule } from '@angular/flex-layout';
import { MatTooltipModule } from '@angular/material/tooltip';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-trending-movies.component',
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
  templateUrl: './trending-movies.component.html',
  styleUrl: './trending-movies.component.css',
})
export class TrendingMoviesComponent implements OnInit {
  trendingMovies: TrendingMovie[] = [];
  filteredTrendingMovies: TrendingMovie[] = [];
  isLoading: boolean = true;
  error: string | null = null;
  sortOrder: string = 'popularity';

  trendingInterval: TrendingInterval = TrendingInterval.Today;

  constructor(private simklService: SimklService) {}

  ngOnInit(): void {
    this.reloadTrendingMovies();
  }

  reloadTrendingMovies(): void {
    this.isLoading = true;
    this.error = null;

    this.simklService.getTrendingMovies(this.trendingInterval).subscribe({
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

    if (this.sortOrder === 'popularity') {
    } else if (this.sortOrder === 'title') {
      this.filteredTrendingMovies.sort((a, b) => a.title.localeCompare(b.title));
    } else if (this.sortOrder === 'release_date') {
      this.filteredTrendingMovies.sort(
        (a, b) => new Date(b.releaseDate).getTime() - new Date(a.releaseDate).getTime()
      );
    } else if (this.sortOrder === 'imdb_rating') {
      this.filteredTrendingMovies.sort((a, b) => (b.imdbRating ?? 0) - (a.imdbRating ?? 0));
    }
  }
}
