import { Component, Input } from '@angular/core';
import { MovieDetails } from '../../../shared/models/movie-details';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { MovieRecommendations } from '../movie-recommendations/movie-recommendations';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-movie-details-card',
  imports: [MatCardModule, MatIconModule, MatSnackBarModule, CommonModule, MovieRecommendations],
  templateUrl: './movie-details-card.html',
  styleUrl: './movie-details-card.css',
})
export class MovieDetailsCard {
  @Input() movieDetails!: MovieDetails;

  constructor(private router: Router, private snackBar: MatSnackBar) {}

  addToWatchlist(simklId: number): void {
    console.log(`Adding movie: ${simklId}`);

    this.snackBar.open('Added to watchlist', '', {
      panelClass: ['custom-snackbar'],
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }

  markWatched(simklId: number): void {
    console.log(`Marking as watched movie: ${simklId}`);

    this.snackBar.open('Marked as watched', '', {
      panelClass: ['custom-snackbar'],
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }
}
