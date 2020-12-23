import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';
import { NEVER, Observable, of } from 'rxjs';
import { FollowService } from 'src/app/service/follow.service';
import { PostService } from 'src/app/service/post.service';
import { UserService } from 'src/app/service/user.service';

import { UserProfileViewComponent } from './user-profile-view.component';

describe('UserProfileViewComponent', () => {
  let component: UserProfileViewComponent;
  let fixture: ComponentFixture<UserProfileViewComponent>;

  let fauxOktaAuthService = {
    getAccessToken(): string {
      return '';
    }
  };

  let fauxHttp = {
    get(url: string, options: { headers: HttpHeaders }): Observable<any> {
      return of(NEVER);
    }
  };

  let fauxActivatedRoute = {
    snapshot: {
      paramMap: {
        get(name: string): string | null {
          return name;
        }
      }
    }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserProfileViewComponent ],
      providers: [
        { provide: HttpClient, useValue: fauxHttp },
        { provide: PostService, useValue: {} },
        { provide: UserService, useValue: {} },
        { provide: FollowService, useValue: {} },
        { provide: ActivatedRoute, useValue: fauxActivatedRoute },
        { provide: OktaAuthService, useValue: fauxOktaAuthService }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserProfileViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
