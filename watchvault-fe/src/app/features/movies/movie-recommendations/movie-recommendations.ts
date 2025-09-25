import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { UserRecommendation } from '../../../shared/models/user-recommendation';

@Component({
  selector: 'app-movie-recommendations',
  imports: [CommonModule, MatCardModule, RouterLink],
  templateUrl: './movie-recommendations.html',
  styleUrl: './movie-recommendations.css',
})
export class MovieRecommendations {
  @Input() recommendations: UserRecommendation[] = [];
}
