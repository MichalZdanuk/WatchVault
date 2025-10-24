export interface WatchListInsights {
  totalWatched: number;
  totalToWatch: number;
  totalFavorites: number;
  averageRuntimeMinutes: number;
  watchedGenresCount: Record<string, number>;
  toWatchGenresCount: Record<string, number>;
  favoriteGenresCount: Record<string, number>;
  averageRuntimePerGenre: Record<string, number>;
  favoriteRate: number;
  totalWatchedRuntimeMinutes: number;
}
