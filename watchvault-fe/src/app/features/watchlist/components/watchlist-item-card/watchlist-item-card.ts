import { Component, Input } from '@angular/core';
import { WatchListItem } from '../../../../shared/models/watchlist-item.model';
import { CommonModule } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { WatchStatus } from '../../../../shared/models/watch-status.enum';
import { RouterLink } from '@angular/router';
import { WatchlistService } from '../../../../core/services/watchlist-service';

@Component({
  selector: 'app-watchlist-item-card',
  imports: [MatIcon, CommonModule, RouterLink],
  templateUrl: './watchlist-item-card.html',
  styleUrl: './watchlist-item-card.css',
})
export class WatchlistItemCard {
  @Input() item!: WatchListItem;
  Status = WatchStatus;

  constructor(private watchListService: WatchlistService) {}

  toggleFavourite(id: string): void {
    this.watchListService.toggleFavourite(id).subscribe({
      next: () => {
        this.item.isFavourite = !this.item.isFavourite;
      },
      error: (err) => {
        console.error('Failed to toggle favourite', err);
      },
    });
  }
}
