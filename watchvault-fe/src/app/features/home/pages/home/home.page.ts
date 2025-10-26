import { Component } from '@angular/core';
import { Hero } from '../../components/hero/hero';
import { Footer } from '../../components/footer/footer';
import { KeyFeatures } from '../../components/key-features/key-features';
import { Community } from '../../components/community/community';

@Component({
  selector: 'app-home.page',
  imports: [Hero, Community, KeyFeatures, Footer],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css',
})
export class HomePage {}
