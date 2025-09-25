import { Component, Input } from '@angular/core';
import { MovieDetails } from '../../../shared/models/movie-details';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { MovieRecommendations } from '../movie-recommendations/movie-recommendations';

@Component({
  selector: 'app-movie-details-card',
  imports: [MatCardModule, MatIconModule, CommonModule, RouterLink, MovieRecommendations],
  templateUrl: './movie-details-card.html',
  styleUrl: './movie-details-card.css',
})
export class MovieDetailsCard {
  @Input() movieDetails!: MovieDetails;

  constructor(private router: Router) {}

  addToWatchlist(simklId: number): void {
    console.log(`Adding movie: ${simklId}`);

    this.router.navigate(['/profile']);
  }

  markWatched(simklId: number): void {
    console.log(`Marking as watched movie: ${simklId}`);

    this.router.navigate(['/profile']);
  }
}
