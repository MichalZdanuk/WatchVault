import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-info-icon',
  imports: [CommonModule, MatIconModule, MatTooltipModule],
  templateUrl: './info-icon.html',
  styleUrl: './info-icon.css',
})
export class InfoIcon {
  @Input() tooltipText: string = 'More info';
  @Input() size: number = 20;
  @Input() color: string = 'var(--color-accent)';
  @Input() position: 'above' | 'below' | 'left' | 'right' = 'right';
}
