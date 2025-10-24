import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenrePieChart } from './genre-pie-chart';

describe('GenrePieChart', () => {
  let component: GenrePieChart;
  let fixture: ComponentFixture<GenrePieChart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenrePieChart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenrePieChart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
