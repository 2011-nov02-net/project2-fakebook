import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LikeService } from 'src/app/service/like.service';
import { LikeViewComponent } from './like-view.component';
import { OktaAuthService } from '@okta/okta-angular';
import { newPost } from '../../model/newpost';


describe('LikeViewComponent', () => {
  let component: LikeViewComponent;
  let fixture: ComponentFixture<LikeViewComponent>;
  let newPost : newPost; //probably have to change for real testing
  let FakeOktaAuthService  = {create(post: newPost) : Promise<newPost> {return Promise.resolve(newPost)} }
  let FakeLikeService = {like(postId: number): any {return 1} }
  
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LikeViewComponent ],
      providers: [
        {provide: LikeService, useValue: FakeLikeService},
        {provide: OktaAuthService, useValue: FakeOktaAuthService},

      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LikeViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
