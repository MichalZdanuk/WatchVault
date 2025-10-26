export interface WatchListSummary {
  id: string;
  totalWatched: number;
  totalToWatch: number;
  totalRuntimeMinutes: number;
  averageRuntimeMinutes: number;
  earliestYearWatched: number | null;
  latestYearWatched: number | null;
  lastWatchedAt: Date | null;
  lastAddedToWatchAt: Date | null;
}
