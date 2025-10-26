import { SimklMovie } from './simkl-movie.model';
import { WatchStatus } from './watch-status.enum';

export interface WatchListItem {
  id: string;
  watchStatus: WatchStatus;
  addedToWatchAt: Date | null;
  watchedAt: Date | null;
  isFavourite: boolean;
  movie: SimklMovie;
}
