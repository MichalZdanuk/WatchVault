import { Component, OnInit } from '@angular/core';
import { AnalyticsService } from '../../../../core/services/analytics.service';
import { StatisticsPeriod } from '../../../../shared/models/statistics-period.enum';
import { WatchListAnalytics } from '../../../../shared/models/watchlist-analytics.model';
import { CommonModule } from '@angular/common';
import { AnalyticsChart } from '../../analytics-chart/analytics-chart';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';
import { WatchListInsights } from '../../../../shared/models/watchlist-insights.model';
import { InsightsGenres } from '../../insights-genres/insights-genres';
import { FavoritesInsights } from '../../favorites-insights/favorites-insights';
import { WeeklyHabits } from '../../weekly-habits/weekly-habits';

@Component({
  selector: 'app-analytics.page',
  imports: [
    CommonModule,
    AnalyticsChart,
    InsightsGenres,
    FavoritesInsights,
    WeeklyHabits,
    InfoIcon,
    LoadingSpinner,
    ErrorMessage,
  ],
  templateUrl: './analytics.page.html',
  styleUrl: './analytics.page.css',
})
export class AnalyticsPage implements OnInit {
  activeMetric: 'watchedCount' | 'runtimeMinutes' = 'watchedCount';
  chartType: 'bar' | 'line' = 'bar';

  lastWeekAnalytics: WatchListAnalytics | null = null;
  lastMonthAnalytics: WatchListAnalytics | null = null;
  thisYearAnalytics: WatchListAnalytics | null = null;
  insights: WatchListInsights | null = null;

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
    const weekStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate() - 6));

    const monthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), 1));
    const monthEnd = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate()));

    const yearStart = new Date(Date.UTC(now.getFullYear(), 0, 1));
    const yearEnd = new Date(Date.UTC(now.getFullYear(), 11, 31));

    Promise.all([
      this.analyticsService.getAnalytics(StatisticsPeriod.Day, weekStart, now).toPromise(),
      this.analyticsService.getAnalytics(StatisticsPeriod.Day, monthStart, monthEnd).toPromise(),
      this.analyticsService.getAnalytics(StatisticsPeriod.Month, yearStart, yearEnd).toPromise(),
      this.analyticsService.getInsights().toPromise(),
    ])
      .then(([week, month, year, insights]) => {
        this.lastWeekAnalytics = week ?? null;
        this.lastMonthAnalytics = month ?? null;
        this.thisYearAnalytics = year ?? null;
        this.insights = insights ?? null;
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

  toggleChartType(): void {
    this.chartType = this.chartType === 'bar' ? 'line' : 'bar';
  }
}
