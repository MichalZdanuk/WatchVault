import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StatisticsPeriod } from '../../shared/models/statistics-period';
import { WatchListAnalytics } from '../../shared/models/watchlist-analytics';
import { WatchListInsights } from '../../shared/models/watchlist-insights';

@Injectable({
  providedIn: 'root',
})
export class AnalyticsService {
  private apiUrlAnalytics: string = environment.apiUrl + '/watchlist/analytics';
  private apiUrlInsights: string = environment.apiUrl + '/watchlist/insights';

  constructor(private http: HttpClient) {}

  getAnalytics(
    statisticsPeriod: StatisticsPeriod,
    startDate: Date,
    endDate: Date
  ): Observable<WatchListAnalytics> {
    const params = new URLSearchParams({
      statisticsPeriod: statisticsPeriod.toString(),
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString(),
    });

    return this.http.get<WatchListAnalytics>(`${this.apiUrlAnalytics}?${params.toString()}`);
  }

  getInsights(): Observable<WatchListInsights> {
    return this.http.get<WatchListInsights>(`${this.apiUrlInsights}`);
  }
}
