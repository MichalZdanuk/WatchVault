import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ChartConfiguration, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-genre-pie-chart',
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './genre-pie-chart.html',
  styleUrl: './genre-pie-chart.css',
})
export class GenrePieChart implements OnChanges {
  @Input() title: string = '';
  @Input() data: Record<string, number> = {};

  public chartType: ChartType = 'pie';
  public chartData: ChartConfiguration['data'] = { labels: [], datasets: [] };
  public chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: 'bottom',
        labels: { color: '#e0e0e0', boxWidth: 14 },
      },
      title: {
        display: true,
        text: '',
        color: '#e0e0e0',
        font: { size: 14 },
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

    this.chartData = {
      labels,
      datasets: [
        {
          data: values,
          backgroundColor: this.generateColors(labels.length),
          borderColor: '#222',
          borderWidth: 1,
        },
      ],
    };

    if (this.chartOptions?.plugins?.title) {
      (this.chartOptions.plugins.title as any).text = this.title;
    }
  }

  private generateColors(count: number): string[] {
    const colors: string[] = [];
    for (let i = 0; i < count; i++) {
      const hue = (i * 360) / count;
      colors.push(`hsl(${hue}, 65%, 55%)`);
    }
    return colors;
  }
}
