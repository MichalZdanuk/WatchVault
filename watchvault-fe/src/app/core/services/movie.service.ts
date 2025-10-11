import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../../shared/models/movie';
import { TrendingInterval } from '../../shared/models/trending-interval';
import { TrendingMovie } from '../../shared/models/trending-movie';
import { MovieDetails } from '../../shared/models/movie-details';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private apiUrl = environment.apiUrl + '/movies';

  constructor(private http: HttpClient) {}

  getMovies(searchTerm: string, page: number = 1, pageSize: number = 10): Observable<Movie[]> {
    const params = new HttpParams()
      .set('searchTerm', searchTerm)
      .set('page', page)
      .set('pageSize', pageSize);

    return this.http.get<Movie[]>(`${this.apiUrl}/search`, { params });
  }

  getMovieDetails(simklId: number): Observable<MovieDetails> {
    return this.http.get<MovieDetails>(`${this.apiUrl}/${simklId}`);
  }

  getTrendingMovies(trendingInterval: TrendingInterval): Observable<TrendingMovie[]> {
    const params = new HttpParams().set('trendingInterval', trendingInterval);

    return this.http.get<TrendingMovie[]>(`${this.apiUrl}/trending`, { params });
  }
}
