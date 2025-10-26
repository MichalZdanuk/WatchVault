import { Component, ViewChild, HostListener } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Navbar } from '../../../shared/components/navbar/navbar';

@Component({
  selector: 'app-main-layout',
  imports: [CommonModule, RouterOutlet, MatSidenavModule, MatIconModule, MatButtonModule, Navbar],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {
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
