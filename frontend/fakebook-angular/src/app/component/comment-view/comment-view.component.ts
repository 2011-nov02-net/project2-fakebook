import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';
import { Comment } from 'src/app/model/comment';

@Component({
  selector: 'app-comment-view',
  templateUrl: './comment-view.component.html',
  styleUrls: ['./comment-view.component.css']
})
export class CommentViewComponent implements OnInit {
  userOwnsComment: boolean;

  user = {
    profilePictureUrl: '',
    fullname: ''
  };

  @Input() comment: Comment | null = null;

  constructor(private authService: AuthService) {
    this.userOwnsComment = false;
  }

  ngOnInit(): void { 
    if(this.comment && this.comment.user) {
      if(this.comment.user.profilePictureUrl) {
        this.user.profilePictureUrl = this.comment.user.profilePictureUrl as string;
        console.log(this.user.profilePictureUrl);
      }

      this.user.fullname = this.comment.user.firstName + " " + this.comment.user.lastName;
    }
  }
}
