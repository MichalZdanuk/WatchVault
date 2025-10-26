import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { UserRecommendation } from '../../models/user-recommendation.model';
import { InfoIcon } from '../../../../shared/components/info-icon/info-icon';

@Component({
  selector: 'app-movie-recommendations',
  imports: [CommonModule, MatCardModule, RouterLink, InfoIcon],
  templateUrl: './movie-recommendations.html',
  styleUrl: './movie-recommendations.css',
})
export class MovieRecommendations {
  @Input() recommendations: UserRecommendation[] = [];
}
