import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { OktaAuthService } from '@okta/okta-angular';

import { NewsfeedService } from './newsfeed.service';

describe('NewsfeedService', () => {
  let service: NewsfeedService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: HttpClient, useValue: {} },
        { provide: OktaAuthService, useValue: {} },
        { provide: NewsfeedService, useValue: {} }
      ]
    });
    service = TestBed.inject(NewsfeedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
