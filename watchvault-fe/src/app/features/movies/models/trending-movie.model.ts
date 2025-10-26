export interface TrendingMovie {
  simklId: number;
  title: string;
  posterUrl: string;
  releaseDate: string;
  imdbRating: number | null;
  runtime: string;
  runtimeMinutes: number;
  overview: string;
  metadata: string;
}
