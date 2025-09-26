import { Component } from '@angular/core';
import { Hero } from '../../hero/hero';
import { Footer } from '../../footer/footer';
import { KeyFeatures } from '../../key-features/key-features';
import { Community } from '../../community/community';

@Component({
  selector: 'app-home.component',
  imports: [Hero, Community, KeyFeatures, Footer],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {}
