import { Component, Input } from '@angular/core';
import { MovieDetails } from '../../models/movie-details.model';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MovieRecommendations } from '../movie-recommendations/movie-recommendations';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { WatchlistService } from '../../../../shared/services/watchlist-service';
import { WatchStatus } from '../../../../shared/models/watch-status.enum';

@Component({
  selector: 'app-movie-details-card',
  imports: [MatCardModule, MatIconModule, MatSnackBarModule, CommonModule, MovieRecommendations],
  templateUrl: './movie-details-card.html',
  styleUrl: './movie-details-card.css',
})
export class MovieDetailsCard {
  @Input() movieDetails!: MovieDetails;

  protected readonly WatchStatus = WatchStatus;

  constructor(private watchlistService: WatchlistService, private snackBar: MatSnackBar) {}

  addToWatchlist(simklId: number): void {
    this.watchlistService.addWatchListItem(simklId, WatchStatus.ToWatch).subscribe({
      next: () => {
        this.movieDetails.watchStatus = WatchStatus.ToWatch;
      },
      error: () => {
        this.showSnackBarError('Failed to add to watch');
      },
    });

    this.snackBar.open('Added to watchlist', '', {
      panelClass: ['custom-snackbar'],
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }

  markWatched(simklId: number): void {
    this.watchlistService.addWatchListItem(simklId, WatchStatus.Watched).subscribe({
      next: () => {
        this.movieDetails.watchStatus = WatchStatus.Watched;
      },
      error: () => {
        this.showSnackBarError('Failed to mark as watched');
      },
    });

    this.snackBar.open('Marked as watched', '', {
      panelClass: ['custom-snackbar'],
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }

  private showSnackBarError(message: string): void {
    this.snackBar.open(message, '', {
      panelClass: ['error-snackbar'],
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }
}
