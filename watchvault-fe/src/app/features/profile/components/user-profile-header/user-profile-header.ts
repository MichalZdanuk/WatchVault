import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserProfile } from '../../models/user-profile.model';

@Component({
  selector: 'app-user-profile-header',
  imports: [CommonModule],
  templateUrl: './user-profile-header.html',
  styleUrl: './user-profile-header.css',
})
export class UserProfileHeader implements OnInit {
  @Input() userProfile!: UserProfile;

  avatarUrl: string | null = null;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadAvatar();
  }

  private loadAvatar(): void {
    this.userService.getUserAvatar().subscribe((url) => {
      this.avatarUrl = url;
    });
  }

  get initials(): string {
    const f = this.userProfile?.firstName ?? '';
    const l = this.userProfile?.lastName ?? '';
    return (
      ((f[0] ?? '') + (l[0] ?? '')).toUpperCase() ||
      this.userProfile?.userName?.slice(0, 2).toUpperCase()
    );
  }
}
