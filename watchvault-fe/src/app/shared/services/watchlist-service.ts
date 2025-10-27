import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WatchListSummary } from '../../features/watchlist/models/watchlist-summary.model';
import { PagedWatchListItems } from '../../features/watchlist/models/paged-watchlist-items.model';
import { WatchListHistory } from '../../features/history/models/watchlist-history.model';
import { WatchStatus } from '../models/watch-status.enum';

@Injectable({
  providedIn: 'root',
})
export class WatchlistService {
  private apiUrl = environment.apiUrl + '/watchlist';

  constructor(private http: HttpClient) {}

  addWatchListItem(simklId: number, watchStatus: WatchStatus): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, {
      simklId: simklId,
      watchStatus: watchStatus,
    });
  }

  getWatchListSummary(): Observable<WatchListSummary> {
    return this.http.get<WatchListSummary>(`${this.apiUrl}`);
  }

  browseWatchListItems(
    watchStatus: WatchStatus | null,
    pageNumber: number = 1,
    pageSize: number = 10
  ): Observable<PagedWatchListItems> {
    let params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);

    if (watchStatus !== null && watchStatus !== undefined) {
      params = params.set('watchStatus', watchStatus);
    }

    return this.http.get<PagedWatchListItems>(`${this.apiUrl}/items`, { params });
  }

  toggleFavourite(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/items/${id}/favourite`, {});
  }

  browseWatchListHistory(
    pageNumber: number = 1,
    pageSize: number = 20
  ): Observable<WatchListHistory> {
    let params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);

    return this.http.get<WatchListHistory>(`${this.apiUrl}/history`, { params });
  }

  updateWatchDate(id: string, date: Date): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/items`, { WatchedAt: date });
  }

  removeWatchListItem(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/items/${id}`);
  }
}
