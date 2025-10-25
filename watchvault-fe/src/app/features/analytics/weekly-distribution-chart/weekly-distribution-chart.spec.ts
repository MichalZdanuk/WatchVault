import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeeklyDistributionChart } from './weekly-distribution-chart';

describe('WeeklyDistributionChart', () => {
  let component: WeeklyDistributionChart;
  let fixture: ComponentFixture<WeeklyDistributionChart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeeklyDistributionChart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeeklyDistributionChart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
