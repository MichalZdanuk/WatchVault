import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Status } from '../../shared/models/status';
import { Observable } from 'rxjs';
import { WatchListSummary } from '../../shared/models/watchlist-summary';
import { PagedWatchListItems } from '../../shared/models/paged-watchlist-items';
import { WatchListHistoryResponse } from '../../shared/models/watchlist-history-response';

@Injectable({
  providedIn: 'root',
})
export class WatchlistService {
  private apiUrl = environment.apiUrl + '/watchlist';

  constructor(private http: HttpClient) {}

  addWatchListItem(simklId: number, status: Status): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, { simklId: simklId, watchStatus: status });
  }

  getWatchListSummary(): Observable<WatchListSummary> {
    return this.http.get<WatchListSummary>(`${this.apiUrl}`);
  }

  browseWatchListItems(
    status: Status | null,
    pageNumber: number = 1,
    pageSize: number = 10
  ): Observable<PagedWatchListItems> {
    let params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);

    if (status !== null && status !== undefined) {
      params = params.set('status', status);
    }

    return this.http.get<PagedWatchListItems>(`${this.apiUrl}/items`, { params });
  }

  toggleFavourite(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/items/${id}/favourite`, {});
  }

  browseWatchListHistory(
    pageNumber: number = 1,
    pageSize: number = 20
  ): Observable<WatchListHistoryResponse> {
    let params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);

    return this.http.get<WatchListHistoryResponse>(`${this.apiUrl}/history`, { params });
  }

  updateWatchDate(id: string, date: Date): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/items`, { WatchedAt: date });
  }

  removeWatchListItem(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/items/${id}`);
  }
}
