import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AverageRuntimeChart } from './average-runtime-chart';

describe('AverageRuntimeChart', () => {
  let component: AverageRuntimeChart;
  let fixture: ComponentFixture<AverageRuntimeChart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AverageRuntimeChart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AverageRuntimeChart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
