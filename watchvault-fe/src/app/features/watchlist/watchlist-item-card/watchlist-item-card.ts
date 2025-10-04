import { Component, Input } from '@angular/core';
import { WatchListItem } from '../../../shared/models/watchlist-item';
import { CommonModule } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { WatchStatus } from '../../../shared/models/watch-status';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-watchlist-item-card',
  imports: [MatIcon, CommonModule, RouterLink],
  templateUrl: './watchlist-item-card.html',
  styleUrl: './watchlist-item-card.css',
})
export class WatchlistItemCard {
  @Input() item!: WatchListItem;
  Status = WatchStatus;

  toggleFavourite(id: string): void {
    this.item.isFavourite = !this.item.isFavourite;
    console.log(`Toggled favourite for watchlist item: ${id}, now: ${this.item.isFavourite}`);
    // TODO: call BE
  }
}
