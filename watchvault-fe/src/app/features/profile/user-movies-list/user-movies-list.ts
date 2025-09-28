import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MovieSummary } from '../../../shared/models/movie-summary';

@Component({
  selector: 'app-user-movies-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './user-movies-list.html',
  styleUrl: './user-movies-list.css',
})
export class UserMoviesList {
  @Input() title = '';
  @Input() movies: MovieSummary[] = [];
  @Input() emptyMessage = 'No movies';

  get shownMovies(): MovieSummary[] {
    return (this.movies ?? []).slice(0, 10);
  }
}
