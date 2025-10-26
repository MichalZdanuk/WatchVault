import { Component, Input } from '@angular/core';
import { WatchListInsights } from '../../../shared/models/watchlist-insights.model';
import { CommonModule } from '@angular/common';
import { WeeklyDistributionChart } from '../weekly-distribution-chart/weekly-distribution-chart';

@Component({
  selector: 'app-weekly-habits',
  imports: [CommonModule, WeeklyDistributionChart],
  templateUrl: './weekly-habits.html',
  styleUrl: './weekly-habits.css',
})
export class WeeklyHabits {
  @Input() insights!: WatchListInsights;
}
