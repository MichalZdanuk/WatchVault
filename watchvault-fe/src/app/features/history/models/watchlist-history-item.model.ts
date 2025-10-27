export interface WatchListHistoryItem {
  id: string;
  watchedAt: Date;
  isFavourite: boolean;
  simklId: number;
  title: string;
  posterUrl: string;
  genres: string[];
}
