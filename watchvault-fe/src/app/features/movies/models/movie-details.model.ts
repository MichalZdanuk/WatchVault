import { WatchStatus } from '../../../shared/models/watch-status.enum';
import { UserRecommendation } from './user-recommendation.model';

export interface MovieDetails {
  simklId: number;
  title: string;
  year: number;
  type: string;
  posterUrl: string;
  fanartUrl: string;
  released: Date | null;
  runtime: number | null;
  imdbRating: number | null;
  director: string;
  overview: string;
  watchStatus: WatchStatus | null;
  genres: string[];
  userRecommendations: UserRecommendation[];
}
