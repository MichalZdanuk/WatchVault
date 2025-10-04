import { SimklMovie } from './simkl-movie';
import { WatchStatus } from './watch-status';

export interface WatchListItem {
  id: string;
  watchStatus: WatchStatus;
  addedToWatchAt: Date | null;
  watchedAt: Date | null;
  isFavourite: boolean;
  movie: SimklMovie;
}
