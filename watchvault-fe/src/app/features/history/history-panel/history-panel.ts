import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HistoryItem } from '../history-item/history-item';
import { CommonModule } from '@angular/common';
import { WatchListHistoryResponse } from '../../../shared/models/watchlist-history-response';

@Component({
  selector: 'app-history-panel',
  imports: [CommonModule, HistoryItem],
  templateUrl: './history-panel.html',
  styleUrl: './history-panel.css',
})
export class HistoryPanel {
  @Input() history!: WatchListHistoryResponse;

  @Output() pageChange = new EventEmitter<number>();

  onPrevPage(): void {
    if (this.history.pageNumber > 1) {
      this.pageChange.emit(this.history.pageNumber - 1);
    }
  }

  onNextPage(): void {
    this.pageChange.emit(this.history.pageNumber + 1);
  }

  onItemDeleted(id: string) {
    this.history.items = this.history.items.filter((x) => x.id !== id);

    if (this.history.items.length === 0 && this.history.pageNumber > 1) {
      this.pageChange.emit(this.history.pageNumber - 1);
    }
  }
}
