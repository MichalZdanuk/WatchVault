import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { MovieCard } from '../../movie-card/movie-card';
import { SimklService } from '../../../../core/services/simkl.service';
import { Movie } from '../../../../shared/models/movie';
import { FlexModule } from '@angular/flex-layout';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { fromEvent } from 'rxjs';
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  switchMap,
  catchError,
} from 'rxjs/operators';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';

@Component({
  selector: 'app-movie-search.component',
  imports: [CommonModule, FlexModule, MovieCard, InfoIcon, MatInputModule, MatFormFieldModule],
  templateUrl: './movie-search.component.html',
  styleUrl: './movie-search.component.css',
})
export class MovieSearchComponent {
  @ViewChild('searchInput', { static: true }) searchInput!: ElementRef;

  movies: Movie[] = [];
  error: string | null = null;
  page: number = 1;
  pageSize: number = 20;

  constructor(private simklService: SimklService) {}

  ngOnInit(): void {
    fromEvent<InputEvent>(this.searchInput.nativeElement, 'keyup')
      .pipe(
        map((event) => (event.target as HTMLInputElement).value.trim().toLowerCase()),
        filter((term) => term.length >= 3),
        debounceTime(400),
        distinctUntilChanged(),
        switchMap((searchTerm) =>
          this.simklService.getMovies(searchTerm, this.page, this.pageSize).pipe(
            catchError((err) => {
              this.error = '⚠️ Failed to search movies. Please try again.';
              console.error(err);
              return [];
            })
          )
        )
      )
      .subscribe((movies) => {
        this.movies = movies;
      });
  }
}
