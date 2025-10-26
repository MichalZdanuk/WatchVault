import { WatchListItem } from './watchlist-item.model';

export interface PagedWatchListItems {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  items: WatchListItem[];
}
