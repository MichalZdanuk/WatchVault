import { Component, OnInit } from '@angular/core';
import { AnalyticsService } from '../../../../core/services/analytics.service';
import { StatisticsPeriod } from '../../../../shared/models/statistics-period';
import { WatchListAnalytics } from '../../../../shared/models/watchlist-analytics';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-analytics.component',
  imports: [CommonModule],
  templateUrl: './analytics.component.html',
  styleUrl: './analytics.component.css',
})
export class AnalyticsComponent implements OnInit {
  constructor(private analyticsService: AnalyticsService) {}
  lastWeekAnalytics: WatchListAnalytics | null = null;
  isLoadingLastWeekAnalytics: boolean = true;
  lastWeekAnalyticsError: string | null = null;

  ngOnInit(): void {
    const now = new Date();
    const start = new Date(now);
    start.setDate(start.getDate() - 6);

    this.analyticsService.getAnalytics(StatisticsPeriod.Day, start, now).subscribe({
      next: (d) => {
        this.lastWeekAnalytics = d;
      },
      error: (err) => {
        this.lastWeekAnalyticsError = '⚠️ Failed to load last week analytics';

        console.error(err);
      },
      complete: () => {
        this.isLoadingLastWeekAnalytics = false;
      },
    });
  }
}
