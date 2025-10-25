import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeeklyHabits } from './weekly-habits';

describe('WeeklyHabits', () => {
  let component: WeeklyHabits;
  let fixture: ComponentFixture<WeeklyHabits>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeeklyHabits]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeeklyHabits);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
