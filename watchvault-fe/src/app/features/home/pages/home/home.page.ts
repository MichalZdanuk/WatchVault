import { Component } from '@angular/core';
import { Hero } from '../../hero/hero';
import { Footer } from '../../footer/footer';
import { KeyFeatures } from '../../key-features/key-features';
import { Community } from '../../community/community';

@Component({
  selector: 'app-home.page',
  imports: [Hero, Community, KeyFeatures, Footer],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css',
})
export class HomePage {}
