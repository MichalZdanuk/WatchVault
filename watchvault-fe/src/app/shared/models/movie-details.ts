import { UserRecommendation } from './user-recommendation';

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
  genres: string[];
  userRecommendations: UserRecommendation[];
}
