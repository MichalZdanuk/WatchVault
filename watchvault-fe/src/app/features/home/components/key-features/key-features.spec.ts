import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KeyFeatures } from './key-features';

describe('KeyFeatures', () => {
  let component: KeyFeatures;
  let fixture: ComponentFixture<KeyFeatures>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KeyFeatures]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KeyFeatures);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
