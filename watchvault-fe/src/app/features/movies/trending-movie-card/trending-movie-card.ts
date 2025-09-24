import { Component, Input } from '@angular/core';
import { TrendingMovie } from '../../../shared/models/trending-movie';
import { MatCardModule } from '@angular/material/card';
import { TruncatePipe } from '../../../shared/pipes/truncate.pipe';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-trending-movie-card',
  imports: [MatCardModule, TruncatePipe, RouterLink, CommonModule],
  templateUrl: './trending-movie-card.html',
  styleUrl: './trending-movie-card.css',
})
export class TrendingMovieCard {
  @Input() trendingMovie!: TrendingMovie;
}
