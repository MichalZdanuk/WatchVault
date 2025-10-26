import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { UserStats } from '../../../shared/models/user-profile.model';

@Component({
  selector: 'app-user-statistics',
  imports: [CommonModule],
  templateUrl: './user-statistics.html',
  styleUrl: './user-statistics.css',
})
export class UserStatistics {
  @Input() stats!: UserStats;
}
