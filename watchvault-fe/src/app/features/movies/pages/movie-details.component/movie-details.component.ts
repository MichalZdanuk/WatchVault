import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MovieDetails } from '../../../../shared/models/movie-details.model';
import { MovieService } from '../../../../core/services/movie.service';
import { CommonModule } from '@angular/common';
import { MovieDetailsCard } from '../../movie-details-card/movie-details-card';
import { MatIconModule } from '@angular/material/icon';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';

@Component({
  selector: 'app-movie-details.component',
  imports: [
    CommonModule,
    MovieDetailsCard,
    MatIconModule,
    RouterLink,
    LoadingSpinner,
    ErrorMessage,
  ],
  templateUrl: './movie-details.component.html',
  styleUrl: './movie-details.component.css',
})
export class MovieDetailsComponent implements OnInit {
  movieDetails: MovieDetails | null = null;
  isLoading: boolean = true;
  error: string | null = null;
  movieId!: number;
  notFound: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private movieService: MovieService) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.movieId = +id;
        this.fetchMovieDetails();
      }
    });
  }

  fetchMovieDetails(): void {
    this.isLoading = true;
    this.error = null;
    this.notFound = false;
    this.movieDetails = null;

    this.movieService.getMovieDetails(this.movieId).subscribe({
      next: (movieDetails) => {
        this.movieDetails = movieDetails;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;

        if (err.status === 404) {
          this.notFound = true;
        } else {
          this.error = '⚠️ Failed to load movie details. Please try again.';
        }
      },
    });
  }
}
