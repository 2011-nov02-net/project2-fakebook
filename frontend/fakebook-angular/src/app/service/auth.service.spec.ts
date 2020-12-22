import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';
import { NEVER } from 'rxjs';

import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;

  let fauxOktaAuthService = {
    $authenticationState: NEVER
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: Router, useValue: {} },
        { provide: OktaAuthService, useValue: fauxOktaAuthService }
      ]
    });
    service = TestBed.inject(AuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
