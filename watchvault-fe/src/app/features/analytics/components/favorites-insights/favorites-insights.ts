import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { WatchListInsights } from '../../models/watchlist-insights.model';
import { GenreChart } from '../genre-chart/genre-chart';

@Component({
  selector: 'app-favorites-insights',
  imports: [CommonModule, GenreChart],
  templateUrl: './favorites-insights.html',
  styleUrl: './favorites-insights.css',
})
export class FavoritesInsights {
  @Input() insights!: WatchListInsights;

  get favoriteRatePercent(): string {
    return (this.insights.favoriteRate * 100).toFixed(1) + '%';
  }

  get totalHoursWatched(): string {
    const hours = this.insights.totalWatchedRuntimeMinutes / 60;
    return hours.toFixed(1);
  }
}
