import { Component, EventEmitter, Output } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AuthStateService } from '../../../core/auth/auth-state.service';

@Component({
  selector: 'app-navbar',
  imports: [
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  @Output() navClicked = new EventEmitter<void>();
  username: string = '';

  constructor(private authState: AuthStateService) {
    const token = authState.getDecodedToken();

    if (token) {
      this.username = token.name;
    }
  }

  onLinkClick() {
    this.navClicked.emit();
  }

  onLogout(): void {
    this.authState.logout();
  }
}
