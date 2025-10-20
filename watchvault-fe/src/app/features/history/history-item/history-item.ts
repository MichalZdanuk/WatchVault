import { Component, Input } from '@angular/core';
import { WatchHistoryItem } from '../../../shared/models/watchlist-history-response';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import { WatchlistService } from '../../../core/services/watchlist-service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-history-item',
  imports: [
    CommonModule,
    RouterLink,
    MatIcon,
    MatDatepickerModule,
    MatInputModule,
    MatButtonModule,
    MatNativeDateModule,
  ],
  providers: [DatePipe],
  templateUrl: './history-item.html',
  styleUrl: './history-item.css',
})
export class HistoryItem {
  @Input() item!: WatchHistoryItem;
  today = new Date();

  constructor(
    private watchlistService: WatchlistService,
    private snackBar: MatSnackBar,
    private datePipe: DatePipe
  ) {}

  onDateSelected(event: any) {
    const selectedDate: Date = event.value;
    if (selectedDate) {
      const now = new Date();
      selectedDate.setHours(
        now.getHours(),
        now.getMinutes(),
        now.getSeconds(),
        now.getMilliseconds()
      );

      this.watchlistService.updateWatchDate(this.item.id, selectedDate).subscribe({
        next: () => {
          this.item.watchedAt = selectedDate;
          const formattedDate = this.datePipe.transform(selectedDate, 'MMM d, y, HH:mm');
          this.snackBar.open(`Changed watch date to: ${formattedDate}`, '', {
            panelClass: ['custom-snackbar'],
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
          });
        },
        error: (err) => console.error('Failed to update date', err),
      });
    }
  }

  onDatepickerClosed() {}
}
