import { Component, OnInit } from '@angular/core';
import { Status } from '../../../../shared/models/status.enum';
import { WatchlistSummary } from '../../watchlist-summary/watchlist-summary';
import { WatchlistItems } from '../../watchlist-items/watchlist-items';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-watchlist.page',
  imports: [InfoIcon, WatchlistSummary, WatchlistItems, MatTabsModule, CommonModule],
  templateUrl: './watchlist.page.html',
  styleUrl: './watchlist.page.css',
})
export class WatchlistPage implements OnInit {
  protected readonly Status = Status;
  selectedTabIndex: number = 0;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params) => {
      const tab = params.get('tab');
      if (tab == 'watched') {
        this.selectedTabIndex = 1;
      } else {
        this.selectedTabIndex = 0;
      }
    });
  }

  onTabChange(index: number): void {
    const tab = index === 1 ? 'watched' : 'toWatch';
    this.selectedTabIndex = index;

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { tab },
      queryParamsHandling: 'merge',
    });
  }
}
