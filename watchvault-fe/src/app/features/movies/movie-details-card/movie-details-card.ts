import { Component, Input } from '@angular/core';
import { MovieDetails } from '../../../shared/models/movie-details';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-movie-details-card',
  imports: [MatCardModule, MatIconModule, CommonModule],
  templateUrl: './movie-details-card.html',
  styleUrl: './movie-details-card.css',
})
export class MovieDetailsCard {
  @Input() movieDetails!: MovieDetails;
}
