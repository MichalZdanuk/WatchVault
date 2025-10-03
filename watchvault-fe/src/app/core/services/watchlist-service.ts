import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Status } from '../../shared/models/status';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class WatchlistService {
  private apiUrl = environment.apiUrl + '/watchlist';

  constructor(private http: HttpClient) {}

  addWatchListItem(simklId: number, status: Status): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, { simklId: simklId, watchStatus: status });
  }
}
