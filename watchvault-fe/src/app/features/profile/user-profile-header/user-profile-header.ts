import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { UserProfile } from '../../../shared/models/user-profile';

@Component({
  selector: 'app-user-profile-header',
  imports: [CommonModule],
  templateUrl: './user-profile-header.html',
  styleUrl: './user-profile-header.css',
})
export class UserProfileHeader {
  @Input() userProfile!: UserProfile;

  get initials(): string {
    const f = this.userProfile?.firstName ?? '';
    const l = this.userProfile?.lastName ?? '';
    return (
      ((f[0] ?? '') + (l[0] ?? '')).toUpperCase() ||
      this.userProfile?.userName?.slice(0, 2).toUpperCase()
    );
  }
}
