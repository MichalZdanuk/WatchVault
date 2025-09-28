import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { UserProfile } from '../../../../shared/models/user-profile';
import { CommonModule } from '@angular/common';
import { UserProfileHeader } from '../../user-profile-header/user-profile-header';
import { UserStatistics } from '../../user-statistics/user-statistics';
import { UserMoviesList } from '../../user-movies-list/user-movies-list';

@Component({
  selector: 'app-profile.component',
  imports: [CommonModule, UserProfileHeader, UserStatistics, UserMoviesList],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  userProfile: UserProfile | null = null;
  isLoading: boolean = true;
  error: string | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.authService.getCurrentUserProfile().subscribe({
      next: (p) => {
        this.userProfile = p;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load profile';
        this.isLoading = false;
        console.error(err);
      },
    });
  }
}
