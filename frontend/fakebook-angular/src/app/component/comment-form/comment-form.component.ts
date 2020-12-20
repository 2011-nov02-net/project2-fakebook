import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  
  comment: CommentFormData;

  constructor(
    private commentService: CommentService,
    private route: ActivatedRoute
  ) { 
    this.comment = {
      content: '',
      postId: this.postId,
      parentCommentId: this.parentCommentId
    };
  }

  ngOnInit(): void { }

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
  }
}
