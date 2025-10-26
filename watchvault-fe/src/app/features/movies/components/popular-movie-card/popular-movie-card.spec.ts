import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopularMovieCard } from './popular-movie-card';

describe('PopularMovieCard', () => {
  let component: PopularMovieCard;
  let fixture: ComponentFixture<PopularMovieCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PopularMovieCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopularMovieCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
