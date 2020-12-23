import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { OktaAuthService } from '@okta/okta-angular';

import { FollowService } from './follow.service';

describe('FollowService', () => {
  let service: FollowService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: HttpClient, useValue: {} },
        { provide: OktaAuthService, useValue: {}}
      ]
    });
    service = TestBed.inject(FollowService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
