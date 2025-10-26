import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsightsGenres } from './insights-genres';

describe('InsightsGenres', () => {
  let component: InsightsGenres;
  let fixture: ComponentFixture<InsightsGenres>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsightsGenres]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InsightsGenres);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
