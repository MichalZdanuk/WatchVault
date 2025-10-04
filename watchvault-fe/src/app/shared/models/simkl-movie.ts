export interface SimklMovie {
  simklId: number;
  title: string;
  year: number;
  posterUrl: string;
  releaseDate: Date | null;
  runtimeMinutes: number | null;
  director: string;
  overview: string;
}
