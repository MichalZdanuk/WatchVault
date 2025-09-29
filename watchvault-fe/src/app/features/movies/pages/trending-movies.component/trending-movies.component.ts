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

@Component({
  selector: 'app-trending-movies.component',
  imports: [
    MatTooltipModule,
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
        this.filteredTrendingMovies = t;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = '⚠️ Failed to load trending movies. Please try again.';
        this.isLoading = false;
        console.error(err);
      },
    });
  }
}
