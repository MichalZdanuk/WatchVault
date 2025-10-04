import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchlistItems } from './watchlist-items';

describe('WatchlistItems', () => {
  let component: WatchlistItems;
  let fixture: ComponentFixture<WatchlistItems>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WatchlistItems]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WatchlistItems);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
