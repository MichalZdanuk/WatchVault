import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileHeader } from '../../components/user-profile-header/user-profile-header';
import { UserStatistics } from '../../components/user-statistics/user-statistics';
import { UserMoviesList } from '../../components/user-movies-list/user-movies-list';
import { LoadingSpinner } from '../../../../shared/components/loading-spinner/loading-spinner';
import { ErrorMessage } from '../../../../shared/components/error-message/error-message';
import { UserService } from '../../services/user.service';
import { UserProfile } from '../../models/user-profile.model';

@Component({
  selector: 'app-profile.page',
  imports: [
    CommonModule,
    UserProfileHeader,
    UserStatistics,
    UserMoviesList,
    LoadingSpinner,
    ErrorMessage,
  ],
  templateUrl: './profile.page.html',
  styleUrl: './profile.page.css',
})
export class ProfilePage implements OnInit {
  userProfile: UserProfile | null = null;
  isLoading: boolean = true;
  error: string | null = null;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.reloadProfile();
  }

  reloadProfile(): void {
    this.isLoading = true;
    this.error = null;
    this.userService.getCurrentUserProfile().subscribe({
      next: (p) => {
        this.userProfile = p;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = '⚠️ Failed to load profile';
        this.isLoading = false;
        console.error(err);
      },
    });
  }
}
