import { Component, Input } from '@angular/core';
import { TrendingMovie } from '../../models/trending-movie.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-popular-movie-card',
  imports: [CommonModule, MatCardModule, RouterLink],
  templateUrl: './popular-movie-card.html',
  styleUrl: './popular-movie-card.css',
})
export class PopularMovieCard {
  @Input() movie!: TrendingMovie;
}
