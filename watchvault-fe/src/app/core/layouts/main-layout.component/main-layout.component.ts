import { Component, ViewChild, HostListener } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component/navbar.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-main-layout.component',
  imports: [
    CommonModule,
    RouterOutlet,
    MatSidenavModule,
    MatIconModule,
    MatButtonModule,
    NavbarComponent,
  ],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css',
})
export class MainLayoutComponent {
  @ViewChild('sidenav') sidenav!: MatSidenav;
  isMobile = false;

  constructor() {
    this.checkScreenSize();
  }

  @HostListener('window:resize')
  onResize() {
    this.checkScreenSize();
  }

  private checkScreenSize() {
    this.isMobile = window.innerWidth < 768;
  }

  toggleSidenav() {
    if (this.isMobile) {
      this.sidenav.toggle();
    }
  }

  closeOnMobile() {
    if (this.isMobile) {
      this.sidenav.close();
    }
  }
}
