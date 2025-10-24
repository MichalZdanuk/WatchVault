import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoritesInsights } from './favorites-insights';

describe('FavoritesInsights', () => {
  let component: FavoritesInsights;
  let fixture: ComponentFixture<FavoritesInsights>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FavoritesInsights]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FavoritesInsights);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
