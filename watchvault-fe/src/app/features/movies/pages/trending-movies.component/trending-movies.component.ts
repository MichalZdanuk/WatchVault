import { Component, OnInit } from '@angular/core';
import { SimklService } from '../../../../core/services/simkl.service';
import { TrendingMovie } from '../../../../shared/models/trending-movie';
import { TrendingInterval } from '../../../../shared/models/trending-interval';
import { CommonModule } from '@angular/common';
import { TrendingMovieCard } from '../../trending-movie-card/trending-movie-card';
import { FlexModule } from '@angular/flex-layout';
import { MatTooltipModule } from '@angular/material/tooltip';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';

@Component({
  selector: 'app-trending-movies.component',
  imports: [MatTooltipModule, CommonModule, FlexModule, TrendingMovieCard, InfoIcon],
  templateUrl: './trending-movies.component.html',
  styleUrl: './trending-movies.component.css',
})
export class TrendingMoviesComponent implements OnInit {
  trendingMovies: TrendingMovie[] = [];
  filteredTrendingMovies: TrendingMovie[] = [];

  trendingInterval: TrendingInterval = TrendingInterval.Today;

  constructor(private simklService: SimklService) {}

  ngOnInit(): void {
    this.simklService.getTrendingMovies(this.trendingInterval).subscribe((trendingMovies) => {
      this.trendingMovies = trendingMovies;
      this.filteredTrendingMovies = trendingMovies;
    });
  }
}
