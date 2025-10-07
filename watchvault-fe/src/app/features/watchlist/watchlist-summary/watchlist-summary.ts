import { Component, OnInit } from '@angular/core';
import { WatchListSummary } from '../../../shared/models/watchlist-summary';
import { WatchlistService } from '../../../core/services/watchlist-service';
import { LoadingSpinner } from '../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../shared/components/error-message/error-message';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-watchlist-summary',
  imports: [LoadingSpinner, ErrorMessage, CommonModule],
  templateUrl: './watchlist-summary.html',
  styleUrl: './watchlist-summary.css',
})
export class WatchlistSummary implements OnInit {
  summary: WatchListSummary | null = null;
  isLoading: boolean = true;
  error: string | null = null;

  constructor(private watchListService: WatchlistService) {}

  ngOnInit(): void {
    this.watchListService.getWatchListSummary().subscribe({
      next: (summary) => {
        this.summary = summary;
        this.isLoading = false;
      },
      error: () => {
        this.error = '⚠️ Failed to load watchlist summary';
        this.isLoading = false;
      },
    });
  }
}
