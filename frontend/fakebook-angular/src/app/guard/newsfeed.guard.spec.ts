import { TestBed } from '@angular/core/testing';

import { NewsfeedGuard } from './newsfeed.guard';

describe('NewsfeedGuard', () => {
  let guard: NewsfeedGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(NewsfeedGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
