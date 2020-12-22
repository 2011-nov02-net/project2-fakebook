import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthService } from 'src/app/service/auth.service';
import { NavigationBehaviorOptions, Router } from '@angular/router';
import { MainViewComponent } from './main-view.component';

describe('MainViewComponent', () => {
  let component: MainViewComponent;
  let fixture: ComponentFixture<MainViewComponent>;
  let FakeAuthService = {isAuthenticated: false};
  let FakeRouterService = {navigateByUrl(url: string, extras?: NavigationBehaviorOptions): Promise<boolean> {return Promise.resolve(true)}  };
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainViewComponent ], 
      providers: [
        {provide: AuthService, useValue: FakeAuthService},
        {provide: Router, useValue: FakeRouterService}
                  ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
