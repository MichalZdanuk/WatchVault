import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserMoviesList } from './user-movies-list';

describe('UserMoviesList', () => {
  let component: UserMoviesList;
  let fixture: ComponentFixture<UserMoviesList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserMoviesList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserMoviesList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
