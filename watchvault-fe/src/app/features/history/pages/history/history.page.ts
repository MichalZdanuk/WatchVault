import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { WatchlistService } from '../../../../core/services/watchlist-service';
import { HistoryPanel } from '../../history-panel/history-panel';
import { WatchListHistory } from '../../../../shared/models/watchlist-history.model';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-history.page',
  imports: [CommonModule, InfoIcon, LoadingSpinner, ErrorMessage, HistoryPanel],
  templateUrl: './history.page.html',
  styleUrl: './history.page.css',
})
export class HistoryPage implements OnInit {
  history: WatchListHistory | null = null;
  pageNumber: number = 1;
  pageSize: number = 20;

  isLoading: boolean = true;
  error: string | null = null;

  constructor(
    private watchlistService: WatchlistService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const page = parseInt(params['page'], 10);
      this.pageNumber = isNaN(page) || page < 1 ? 1 : page;
      this.loadHistory(this.pageNumber, this.pageSize);
    });
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

  onPageChange(newPage: number) {
    // Update the URL query param, trigger reload via subscription
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { page: newPage },
      queryParamsHandling: 'merge',
    });
  }
}
