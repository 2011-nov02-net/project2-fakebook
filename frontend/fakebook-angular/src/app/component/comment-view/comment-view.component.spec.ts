import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommentService } from 'src/app/service/comment.service';

import { CommentViewComponent } from './comment-view.component';

describe('CommentViewComponent', () => {
  let component: CommentViewComponent;
  let fixture: ComponentFixture<CommentViewComponent>;

  let fauxCommentService = {
    delete(comment: Comment): Promise<number> {
      return Promise.resolve(-1);
    }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CommentViewComponent ],
      providers: [{ provide: CommentService, useValue: fauxCommentService}]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CommentViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
