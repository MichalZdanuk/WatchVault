import { MovieSummary } from './movie-summary.model';

export interface UserProfile {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  statistics: UserStats;
  watched: MovieSummary[];
  toWatch: MovieSummary[];
}

export interface UserStats {
  totalWatched: number;
  totalToWatch: number;
  totalFavorites: number;
}
