import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieDetailsCard } from './movie-details-card';

describe('MovieDetailsCard', () => {
  let component: MovieDetailsCard;
  let fixture: ComponentFixture<MovieDetailsCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MovieDetailsCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieDetailsCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
