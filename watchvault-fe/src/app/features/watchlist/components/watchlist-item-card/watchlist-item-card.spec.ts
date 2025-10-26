import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchlistItemCard } from './watchlist-item-card';

describe('WatchlistItemCard', () => {
  let component: WatchlistItemCard;
  let fixture: ComponentFixture<WatchlistItemCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WatchlistItemCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WatchlistItemCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
