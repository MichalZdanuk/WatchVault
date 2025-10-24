import { Component, Input } from '@angular/core';
import { WatchListInsights } from '../../../shared/models/watchlist-insights';
import { CommonModule } from '@angular/common';
import { GenrePieChart } from '../genre-pie-chart/genre-pie-chart';
import { AverageRuntimeChart } from '../average-runtime-chart/average-runtime-chart';

@Component({
  selector: 'app-insights-genres',
  imports: [CommonModule, GenrePieChart, AverageRuntimeChart],
  templateUrl: './insights-genres.html',
  styleUrl: './insights-genres.css',
})
export class InsightsGenres {
  @Input() insights!: WatchListInsights;
}
