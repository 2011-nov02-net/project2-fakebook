import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Comment } from 'src/app/model/comment';
import { CommentService } from 'src/app/service/comment.service';

@Component({
  selector: 'app-comment-view',
  templateUrl: './comment-view.component.html',
  styleUrls: ['./comment-view.component.css']
})
export class CommentViewComponent implements OnInit {
  user = {
    profilePictureUrl: '',
    fullname: ''
  };

  @Input() comment: Comment | null = null;
  @Input() currentUserId!: number;

  @Output() delete = new EventEmitter<Comment>();

  constructor(private commentService: CommentService) { }

  ngOnInit(): void { 
    if(this.comment && this.comment.user) {
      if(this.comment.user.profilePictureUrl) {
        this.user.profilePictureUrl = this.comment.user.profilePictureUrl as string;
        console.log(this.user.profilePictureUrl);
      }

      this.user.fullname = this.comment.user.firstName + " " + this.comment.user.lastName;
    }
  }

  deleteComment(comment: Comment) {
    console.log(comment);
    if(comment && comment.id !== undefined) {
      this.commentService.delete(comment);
      this.delete.emit(comment);
    }
  }
}
