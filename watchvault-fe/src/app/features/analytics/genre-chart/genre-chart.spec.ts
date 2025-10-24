import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenreChart } from './genre-chart';

describe('GenrePieChart', () => {
  let component: GenreChart;
  let fixture: ComponentFixture<GenreChart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenreChart],
    }).compileComponents();

    fixture = TestBed.createComponent(GenreChart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
