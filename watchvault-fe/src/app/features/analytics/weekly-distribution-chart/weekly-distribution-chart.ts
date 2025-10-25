import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ChartConfiguration, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-weekly-distribution-chart',
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './weekly-distribution-chart.html',
  styleUrl: './weekly-distribution-chart.css',
})
export class WeeklyDistributionChart implements OnChanges {
  @Input() title: string = '';
  @Input() data: Record<string, number> = {};
  @Input() chartType: ChartType = 'bar';

  public chartData: ChartConfiguration['data'] = { labels: [], datasets: [] };
  public chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    elements: {
      point: {
        radius: 0.5,
      },
    },
    plugins: {
      title: {
        display: true,
        text: '',
        color: '#e0e0e0',
        font: { size: 15, weight: 'bold' },
      },
      legend: {
        display: false,
      },
    },
    scales: {
      r: {
        angleLines: { color: '#333' },
        grid: { color: '#444' },
        ticks: {
          color: '#aaa',
          showLabelBackdrop: false,
          stepSize: 5,
        },
        pointLabels: { color: '#e0e0e0', font: { size: 13 } },
      },
    },
  };

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data']) {
      this.updateChart();
    }
  }

  updateChart(): void {
    if (!this.data || Object.keys(this.data).length === 0) return;

    const labels = Object.keys(this.data);
    const valuesRaw = Object.values(this.data);
    const sum = valuesRaw.reduce((sum, current) => sum + current, 0);
    const values = sum > 0 ? valuesRaw.map((x) => Math.round((x / sum) * 100)) : valuesRaw;

    this.chartData = {
      labels,
      datasets: [
        {
          data: values,
          backgroundColor: 'rgba(155, 89, 182, 0.35)',
          borderColor: 'rgba(155, 89, 182, 1)',
          borderWidth: 2,
          pointBackgroundColor: 'rgba(155, 89, 182, 1)',
          pointBorderColor: '#fff',
          pointHoverBackgroundColor: '#fff',
          pointHoverBorderColor: 'rgba(155, 89, 182, 1)',
        },
      ],
    };

    if (this.chartOptions?.plugins?.title) {
      (this.chartOptions.plugins.title as any).text = this.title;
    }
  }
}
