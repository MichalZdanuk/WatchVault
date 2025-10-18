import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StatisticsPeriod } from '../../shared/models/statistics-period';
import { WatchListAnalytics } from '../../shared/models/watchlist-analytics';

@Injectable({
  providedIn: 'root',
})
export class AnalyticsService {
  private apiUrl: string = environment.apiUrl + '/watchlist/analytics';

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

    return this.http.get<WatchListAnalytics>(`${this.apiUrl}?${params.toString()}`);
  }
}
