import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PostService } from './post.service';
import { HttpClient } from '@angular/common/http';
import { OktaAuthService } from '@okta/okta-angular';

describe('PostService', () => {
  let service: PostService;

  let fakeHttpClient = {
    
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: HttpClient, useValue: {} },
        { provide: OktaAuthService, useValue: {} }
      ]
    });
    service = TestBed.inject(PostService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
