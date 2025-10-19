import { Component, Input } from '@angular/core';
import { WatchHistoryItem } from '../../../shared/models/watchlist-history-response';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-history-item',
  imports: [CommonModule, RouterLink, MatIcon],
  templateUrl: './history-item.html',
  styleUrl: './history-item.css',
})
export class HistoryItem {
  @Input() item!: WatchHistoryItem;

  onEditWatchDate(item: WatchHistoryItem) {
    console.log(`Edit watch date for ${item.title}`);
  }
}
