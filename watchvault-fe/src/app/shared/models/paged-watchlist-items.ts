import { WatchListItem } from './watchlist-item';

export interface PagedWatchListItems {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  items: WatchListItem[];
}
