import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ChartConfiguration, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-average-runtime-chart',
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './average-runtime-chart.html',
  styleUrl: './average-runtime-chart.css',
})
export class AverageRuntimeChart implements OnChanges {
  @Input() title: string = '';
  @Input() data: Record<string, number> = {};

  public chartType: ChartType = 'bar';
  public chartData: ChartConfiguration['data'] = { labels: [], datasets: [] };
  public chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    indexAxis: 'y',
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      title: { display: true, text: '', color: '#e0e0e0' },
    },
    scales: {
      x: {
        ticks: { color: '#ccc' },
        grid: { color: 'rgba(255,255,255,0.05)' },
      },
      y: {
        ticks: { color: '#ccc' },
        grid: { color: 'rgba(255,255,255,0.05)' },
      },
    },
  };

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data']) {
      this.updateChart();
    }
  }

  private updateChart(): void {
    if (!this.data || Object.keys(this.data).length === 0) return;

    const entries = Object.entries(this.data)
      .sort(([, a], [, b]) => b - a)
      .slice(0, 10);

    const labels = entries.map(([key]) => key);
    const values = entries.map(([, value]) => value);

    const accent = getComputedStyle(document.documentElement)
      .getPropertyValue('--color-accent')
      .trim();
    const accentShadow = getComputedStyle(document.documentElement)
      .getPropertyValue('--color-accent-shadow')
      .trim();

    this.chartData = {
      labels,
      datasets: [
        {
          label: 'Average Runtime (minutes)',
          data: values,
          backgroundColor: accentShadow,
          borderColor: accent,
          borderWidth: 2,
          borderRadius: 6,
          hoverBackgroundColor: accent,
        },
      ],
    };

    if (this.chartOptions?.plugins?.title) {
      (this.chartOptions.plugins.title as any).text = this.title;
    }
  }
}
