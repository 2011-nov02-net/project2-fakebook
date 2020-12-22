import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { OktaAuthService } from '@okta/okta-angular';
import { newPost } from '../../model/newpost';
import { NavigationBehaviorOptions } from '@angular/router';
import { RouterTestingModule } from "@angular/router/testing";
import { CommentFormComponent } from './comment-form.component';

describe('CommentFormComponent', () => {
  let component: CommentFormComponent;
  let fixture: ComponentFixture<CommentFormComponent>;
  let newPost : newPost;

  let FakeOktaAuthService  = {create(post: newPost) : Promise<newPost> {return Promise.resolve(newPost)} }

  beforeEach(async () => {
    
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule], 

      declarations: [ CommentFormComponent ],
      providers: [        
        {provide: OktaAuthService, useValue: FakeOktaAuthService},

      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    
    
    fixture = TestBed.createComponent(CommentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
