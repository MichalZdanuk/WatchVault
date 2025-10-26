import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';
import { WatchListAnalytics } from '../../../shared/models/watchlist-analytics.model';
import { ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-analytics-chart',
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './analytics-chart.html',
  styleUrl: './analytics-chart.css',
})
export class AnalyticsChart implements OnChanges {
  @Input() title: string = '';
  @Input() analyticsData: WatchListAnalytics | null = null;
  @Input() metric: 'watchedCount' | 'runtimeMinutes' = 'watchedCount';
  @Input() type: 'bar' | 'line' = 'bar';

  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  public chartData: ChartConfiguration['data'] = { labels: [], datasets: [] };
  public chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      title: { display: true, text: '', color: '#e0e0e0', font: { size: 15, weight: 'bold' } },
    },
    scales: {
      x: { ticks: { color: '#ccc' }, grid: { color: 'rgba(255,255,255,0.05)' } },
      y: { ticks: { color: '#ccc' }, grid: { color: 'rgba(255,255,255,0.05)' } },
    },
  };

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['analyticsData'] || changes['metric']) {
      this.updateChart();
    }
  }

  private updateChart(): void {
    if (!this.analyticsData) return;

    const accent = getComputedStyle(document.documentElement)
      .getPropertyValue('--color-accent')
      .trim();
    const accentShadow = getComputedStyle(document.documentElement)
      .getPropertyValue('--color-accent-shadow')
      .trim();

    const labels = this.analyticsData.data.map((d) => d.periodLabel);
    const values =
      this.metric === 'watchedCount'
        ? this.analyticsData.data.map((d) => d.watchedCount)
        : this.analyticsData.data.map((d) => d.totalRuntimeMinutes);

    this.chartData = {
      labels,
      datasets: [
        {
          data: values,
          label: this.metric === 'watchedCount' ? 'Movies Watched' : 'Total Runtime (min)',
          backgroundColor: accentShadow,
          borderColor: accent,
          borderWidth: 2,
          borderRadius: 4,
          hoverBackgroundColor: accent,
          tension: this.type === 'line' ? 0.4 : 0,
        },
      ],
    };

    if (this.chartOptions?.plugins?.title) {
      (this.chartOptions?.plugins.title as any).text = this.title;
    }

    this.chart?.update();
  }
}
