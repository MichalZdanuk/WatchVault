import { Component, Input, OnInit } from '@angular/core';
import { Status } from '../../../../shared/models/status.enum';
import { WatchListItem } from '../../models/watchlist-item.model';
import { CommonModule } from '@angular/common';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';
import { WatchlistService } from '../../../../shared/services/watchlist-service';
import { WatchlistItemCard } from '../watchlist-item-card/watchlist-item-card';

@Component({
  selector: 'app-watchlist-items',
  imports: [LoadingSpinner, ErrorMessage, WatchlistItemCard, CommonModule],
  templateUrl: './watchlist-items.html',
  styleUrl: './watchlist-items.css',
})
export class WatchlistItems implements OnInit {
  @Input() status!: Status;

  items: WatchListItem[] = [];
  isLoading: boolean = true;
  error: string | null = null;
  pageNumber: number = 1;
  pageSize: number = 12;
  totalCount: number = 0;

  constructor(private watchListService: WatchlistService) {}

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    this.isLoading = true;
    this.error = null;

    this.watchListService
      .browseWatchListItems(this.status, this.pageNumber, this.pageSize)
      .subscribe({
        next: (paged) => {
          this.items = paged.items;
          this.totalCount = paged.totalCount;
          this.isLoading = false;
        },
        error: () => {
          this.error = '⚠️ Failed to load watchlist items.';
          this.isLoading = false;
        },
      });
  }

  onPageChange(page: number): void {
    this.pageNumber = page;
    this.loadItems();
  }
}
