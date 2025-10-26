import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { MovieCard } from '../../movie-card/movie-card';
import { PopularMovieCard } from '../../popular-movie-card/popular-movie-card';
import { MovieService } from '../../../../core/services/movie.service';
import { Movie } from '../../../../shared/models/movie.model';
import { FlexModule } from '@angular/flex-layout';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';
import { TrendingMovie } from '../../../../shared/models/trending-movie.model';
import { TrendingInterval } from '../../../../shared/models/trending-interval.enum';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { fromEvent, of, Subscription } from 'rxjs';
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  switchMap,
  tap,
  catchError,
  map,
} from 'rxjs/operators';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-movie-search.component',
  imports: [
    CommonModule,
    FlexModule,
    MovieCard,
    PopularMovieCard,
    InfoIcon,
    LoadingSpinner,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
  ],
  templateUrl: './movie-search.component.html',
  styleUrl: './movie-search.component.css',
})
export class MovieSearchComponent {
  @ViewChild('searchInput', { static: true }) searchInput!: ElementRef;

  movies: Movie[] = [];
  popularMovies: TrendingMovie[] = [];
  searchTerm: string = '';
  error: string | null = null;
  isLoading = false;
  page: number = 1;
  pageSize: number = 20;

  private subscription = new Subscription();

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadPopularMovies();
  }

  ngAfterViewInit(): void {
    this.setupSearchListener();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private setupSearchListener(): void {
    const sub = fromEvent<InputEvent>(this.searchInput.nativeElement, 'input')
      .pipe(
        map((event) => (event.target as HTMLInputElement).value.trim()),
        debounceTime(400),
        distinctUntilChanged(),
        tap((value) => {
          this.searchTerm = value;
          this.error = null;
          if (value.length < 3) {
            this.movies = [];
          }
        }),
        filter((value) => value.length >= 3),
        tap(() => (this.isLoading = true)),

        switchMap((value) =>
          this.movieService.getMovies(value, this.page, this.pageSize).pipe(
            catchError((err) => {
              console.error('API error:', err);
              this.error = `Could not find any results for "${this.searchTerm}".`;
              return of([]);
            })
          )
        ),
        tap(() => (this.isLoading = false)),
        catchError((err) => {
          console.error('Stream crashed:', err);
          this.isLoading = false;
          this.error = 'Unexpected error. Please try again.';
          return of([]);
        })
      )
      .subscribe((movies) => {
        this.movies = movies;
        if (!movies.length && this.searchTerm.length >= 3) {
          this.error = `Could not find any results for "${this.searchTerm}".`;
        }
      });

    this.subscription.add(sub);
  }

  private loadPopularMovies(): void {
    this.movieService.getTrendingMovies(TrendingInterval.Today).subscribe({
      next: (movies) => {
        this.popularMovies = movies;
      },
      error: (err) => {
        this.error = '⚠️ Failed to load popular movies. Please try again.';
        console.error(err);
      },
    });
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.movies = [];
    this.error = null;

    if (this.searchInput) {
      this.searchInput.nativeElement.value = '';
    }
  }
}
