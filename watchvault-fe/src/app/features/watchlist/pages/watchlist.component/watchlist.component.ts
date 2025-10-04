import { Component } from '@angular/core';
import { Status } from '../../../../shared/models/status';
import { WatchlistSummary } from '../../watchlist-summary/watchlist-summary';
import { WatchlistItems } from '../../watchlist-items/watchlist-items';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';

@Component({
  selector: 'app-watchlist.component',
  imports: [InfoIcon, WatchlistSummary, WatchlistItems, MatTabsModule, CommonModule],
  templateUrl: './watchlist.component.html',
  styleUrl: './watchlist.component.css',
})
export class WatchlistComponent {
  protected readonly Status = Status;
}
