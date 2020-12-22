import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommentFormData } from 'src/app/model/comment-form-data';
import { CommentService } from 'src/app/service/comment.service';

@Component({
  selector: 'app-comment-form',
  templateUrl: './comment-form.component.html',
  styleUrls: ['./comment-form.component.css']
})
export class CommentFormComponent implements OnInit {
  @Input() postId!: number;
  @Input() parentCommentId: number | undefined;
  
  comment: CommentFormData = { content: '', postId: -1, parentCommentId: undefined };

  constructor(
    private commentService: CommentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void { 
    this.comment.postId = this.postId;
    this.comment.parentCommentId = this.parentCommentId;
  }

  getUserId(): number {
    let id = -1;

    let idStr = this.route.snapshot.paramMap.get('id');
    if(idStr) {
      id = +idStr;
    }

    return id;
  }

  postComment(comment: CommentFormData): void {
    // create a comment from the current data
    let userId = this.getUserId();
    
    this.commentService.create({
      id: 0,
      userId: userId,
      postId: comment.postId,
      content: comment.content,
      parentCommentId: comment.parentCommentId,
      user: undefined,
      createdAt: undefined,
      childCommentIds: []
    });

    this.comment.content = '';
    this.router.navigateByUrl('newsfeed/refresh');
  }
}
