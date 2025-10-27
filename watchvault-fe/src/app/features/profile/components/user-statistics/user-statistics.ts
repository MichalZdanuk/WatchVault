import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { UserOverallStatistics } from '../../models/user-overall-statistics.model';

@Component({
  selector: 'app-user-statistics',
  imports: [CommonModule],
  templateUrl: './user-statistics.html',
  styleUrl: './user-statistics.css',
})
export class UserStatistics {
  @Input() userOverallStatistics!: UserOverallStatistics;
}
