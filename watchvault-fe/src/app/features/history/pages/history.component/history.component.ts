import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { WatchlistService } from '../../../../core/services/watchlist-service';
import { HistoryPanel } from '../../history-panel/history-panel';
import { WatchListHistoryResponse } from '../../../../shared/models/watchlist-history-response';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';

@Component({
  selector: 'app-history.component',
  imports: [CommonModule, InfoIcon, LoadingSpinner, ErrorMessage, HistoryPanel],
  templateUrl: './history.component.html',
  styleUrl: './history.component.css',
})
export class HistoryComponent implements OnInit {
  history: WatchListHistoryResponse | null = null;
  pageNumber: number = 1;
  pageSize: number = 20;

  isLoading: boolean = true;
  error: string | null = null;

  constructor(private watchlistService: WatchlistService) {}

  ngOnInit(): void {
    this.loadHistory(this.pageNumber, this.pageSize);
  }

  loadHistory(pageNumber: number, pageSize: number): void {
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
    this.isLoading = true;
    this.error = null;

    this.watchlistService.browseWatchListHistory(this.pageNumber, this.pageSize).subscribe({
      next: (x) => {
        this.history = x;
        this.isLoading = false;
      },
      error: () => {
        this.error = '⚠️ Failed to load history';
        this.isLoading = false;
      },
    });
  }
}
