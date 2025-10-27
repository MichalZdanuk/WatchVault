import { MostWatchedDay } from './most-watched-day.model';

export interface WatchListInsights {
  totalWatched: number;
  totalToWatch: number;
  totalFavorites: number;
  favoriteRate: number;
  averageRuntimeMinutes: number;
  totalWatchedRuntimeMinutes: number;
  watchedGenresCount: Record<string, number>;
  toWatchGenresCount: Record<string, number>;
  favoriteGenresCount: Record<string, number>;
  averageRuntimePerGenre: Record<string, number>;
  watchedDayOfWeekDistribution: Record<string, number>;
  mostWatchedDay: MostWatchedDay;
}
