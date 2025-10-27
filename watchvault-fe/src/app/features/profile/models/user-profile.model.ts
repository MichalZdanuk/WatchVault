import { MovieSummary } from './movie-summary.model';
import { UserOverallStatistics } from './user-overall-statistics.model';

export interface UserProfile {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  userOverallStatistics: UserOverallStatistics;
  watched: MovieSummary[];
  toWatch: MovieSummary[];
}
