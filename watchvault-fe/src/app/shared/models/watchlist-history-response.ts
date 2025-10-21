import { Status } from './status';

export interface WatchListHistoryResponse {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  items: WatchHistoryItem[];
}

export interface WatchHistoryItem {
  id: string;
  watchedAt: Date;
  isFavourite: boolean;
  simklId: number;
  title: string;
  posterUrl: string;
  genres: string[];
}
