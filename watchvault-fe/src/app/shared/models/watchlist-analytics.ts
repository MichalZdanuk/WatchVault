export interface WatchListAnalytics {
  period: string;
  data: WatchListAnalyticsRecord[];
}

export interface WatchListAnalyticsRecord {
  periodLabel: string;
  watchedCount: number;
  totalRuntimeMinutes: number;
}
