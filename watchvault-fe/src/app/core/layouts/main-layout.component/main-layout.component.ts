import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component/navbar.component';

@Component({
  selector: 'app-main-layout.component',
  imports: [RouterOutlet, MatSidenavModule, NavbarComponent],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css',
})
export class MainLayoutComponent {}
