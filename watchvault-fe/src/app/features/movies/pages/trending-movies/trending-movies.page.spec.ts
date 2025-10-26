import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrendingMoviesPage } from './trending-movies.page';

describe('TrendingMoviesPage', () => {
  let component: TrendingMoviesPage;
  let fixture: ComponentFixture<TrendingMoviesPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrendingMoviesPage],
    }).compileComponents();

    fixture = TestBed.createComponent(TrendingMoviesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
