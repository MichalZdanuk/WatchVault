import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MovieDetails } from '../../../../shared/models/movie-details';
import { SimklService } from '../../../../core/services/simkl.service';
import { CommonModule } from '@angular/common';
import { MovieDetailsCard } from '../../movie-details-card/movie-details-card';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-movie-details.component',
  imports: [CommonModule, MovieDetailsCard, MatIconModule, RouterLink],
  templateUrl: './movie-details.component.html',
  styleUrl: './movie-details.component.css',
})
export class MovieDetailsComponent implements OnInit {
  movieDetails!: MovieDetails;
  isLoading: boolean = true;

  constructor(private activatedRoute: ActivatedRoute, private simklService: SimklService) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.isLoading = true;
        this.simklService.getMovieDetails(+id).subscribe((movieDetails) => {
          this.movieDetails = movieDetails;
          this.isLoading = false;
        });
      }
    });
  }
}
