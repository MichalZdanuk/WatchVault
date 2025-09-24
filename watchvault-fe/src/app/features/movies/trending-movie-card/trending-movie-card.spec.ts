import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrendingMovieCard } from './trending-movie-card';

describe('TrendingMovieCard', () => {
  let component: TrendingMovieCard;
  let fixture: ComponentFixture<TrendingMovieCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrendingMovieCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrendingMovieCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
