import { WatchListHistoryItem } from './watchlist-history-item.model';

export interface WatchListHistory {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  items: WatchListHistoryItem[];
}
