import { Component, OnInit } from '@angular/core';
import { AnalyticsService } from '../../../../core/services/analytics.service';
import { StatisticsPeriod } from '../../../../shared/models/statistics-period';
import { WatchListAnalytics } from '../../../../shared/models/watchlist-analytics';
import { CommonModule } from '@angular/common';
import { AnalyticsChart } from '../../analytics-chart/analytics-chart';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';

@Component({
  selector: 'app-analytics.component',
  imports: [CommonModule, AnalyticsChart, InfoIcon, LoadingSpinner, ErrorMessage],
  templateUrl: './analytics.component.html',
  styleUrl: './analytics.component.css',
})
export class AnalyticsComponent implements OnInit {
  activeMetric: 'watchedCount' | 'runtimeMinutes' = 'watchedCount';

  lastWeekAnalytics: WatchListAnalytics | null = null;
  lastMonthAnalytics: WatchListAnalytics | null = null;
  thisYearAnalytics: WatchListAnalytics | null = null;

  isLoading: boolean = true;
  errorMessage: string | null = null;

  constructor(private analyticsService: AnalyticsService) {}

  ngOnInit(): void {
    this.loadAllAnalytics();
  }

  protected loadAllAnalytics(): void {
    this.isLoading = true;
    this.errorMessage = null;

    const now = new Date();

    const weekStart = new Date(now);
    weekStart.setDate(weekStart.getDate() - 6);

    const monthStart = new Date(now);
    monthStart.setDate(monthStart.getDate() - 27);

    const yearStart = new Date(now.getFullYear(), 0, 1);
    const yearEnd = new Date(now.getFullYear(), 11, 31);

    Promise.all([
      this.analyticsService.getAnalytics(StatisticsPeriod.Day, weekStart, now).toPromise(),
      this.analyticsService.getAnalytics(StatisticsPeriod.Day, monthStart, now).toPromise(),
      this.analyticsService.getAnalytics(StatisticsPeriod.Month, yearStart, yearEnd).toPromise(),
    ])
      .then(([week, month, year]) => {
        this.lastWeekAnalytics = week ?? null;
        this.lastMonthAnalytics = month ?? null;
        this.thisYearAnalytics = year ?? null;
      })
      .catch((err) => {
        console.error(err);
        this.errorMessage = '⚠️ Failed to load analytics data';
      })
      .finally(() => {
        this.isLoading = false;
      });
  }

  toggleMetric(): void {
    this.activeMetric = this.activeMetric === 'watchedCount' ? 'runtimeMinutes' : 'watchedCount';
  }
}
