import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WatchHistoryItem } from '../../../shared/models/watchlist-history.model';
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
  @Output() deleted = new EventEmitter<string>();
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

  confirmDelete() {
    if (confirm(`Are you sure you want to remove "${this.item.title}" from your history?`)) {
      this.watchlistService.removeWatchListItem(this.item.id).subscribe({
        next: () => {
          this.snackBar.open(`Removed "${this.item.title}" from history`, '', {
            panelClass: ['custom-snackbar'],
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
          });
          this.deleted.emit(this.item.id);
        },
        error: (err) => {
          console.error('Failed to remove item', err);
          this.snackBar.open('Failed to remove item', '', {
            panelClass: ['error-snackbar'],
            duration: 3000,
          });
        },
      });
    }
  }
}
