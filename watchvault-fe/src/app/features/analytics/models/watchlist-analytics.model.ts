import { WatchListAnalyticsRecord } from './watchlist-analytics-record.model';

export interface WatchListAnalytics {
  period: string;
  data: WatchListAnalyticsRecord[];
}
