import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchlistSummary } from './watchlist-summary';

describe('WatchlistSummary', () => {
  let component: WatchlistSummary;
  let fixture: ComponentFixture<WatchlistSummary>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WatchlistSummary]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WatchlistSummary);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
