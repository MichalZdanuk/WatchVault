import { Status } from './status';

export interface WatchListHistoryResponse {
  pageNumber: number;
  pageSize: number;
  items: WatchHistoryItem[];
}

export interface WatchHistoryItem {
  id: string;
  addedToWatchAt: Date | null;
  watchedAt: Date | null;
  isFavourite: boolean;
  simklId: number;
  title: string;
  posterUrl: string;
  genres: string[];
  status: Status;
}
