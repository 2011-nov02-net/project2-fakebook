import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserService } from 'src/app/service/user.service';
import { UploadService } from '../../service/upload.service';
import { newPost } from '../../model/newpost';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from 'src/app/service/auth.service';
import { OktaAuthService } from '@okta/okta-angular';



import { NewPostFormComponent } from './new-post-form.component';

describe('NewPostFormComponent', () => {
  let component: NewPostFormComponent;
  let fixture: ComponentFixture<NewPostFormComponent>;
  let FakeAuthService = {isAuthenticated: false};
  let FakeGetUser = { getUser(): void {} };
  let newPost : newPost;
  let FakeOktaAuthService  = {create(post: newPost) : Promise<newPost> {return Promise.resolve(newPost)} }
  let FakeUploadService = {upload(data: FormData): Promise<{path:string,userId:number}> {return Promise.resolve({path:"string", userId:2}) }}

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule], 

      declarations: [ NewPostFormComponent ],
      providers: [
        {provide: UserService, useValue: FakeGetUser},
        {provide: UploadService, useValue: FakeUploadService},
        {provide: AuthService, useValue: FakeAuthService},
        {provide: OktaAuthService, useValue: FakeOktaAuthService},
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPostFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
