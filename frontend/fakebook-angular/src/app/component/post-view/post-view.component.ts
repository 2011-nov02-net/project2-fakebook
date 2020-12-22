import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';
import { LikeService } from 'src/app/service/like.service';
import { Post } from '../../model/post';
import { Comment } from 'src/app/model/comment';
import { PostService } from 'src/app/service/post.service';

@Component({
  selector: 'app-post-view',
  templateUrl: './post-view.component.html',
  styleUrls: ['./post-view.component.css'],
})
export class PostViewComponent implements OnInit {
  @Input() post: Post | null = null;
  @Input() userid: number = 0;

  @Output() notifyComment: EventEmitter<string> = new EventEmitter<string>();

  constructor(
    private route: ActivatedRoute,
    private postService: PostService
  ) {
  }

  ngOnInit(): void {
    this.userLiked();
  }

  getUserId(): number {
    let id = -1;

    let idStr = this.route.snapshot.paramMap.get('id');
    if (idStr) {
      id = +idStr;
    }

    return id;
  }

  deletePost(post: Post) {
    this.post=null;
    this.postService.delete(post.id);
  }

  userLiked() {
    if (this.post) {
      this.post.liked = this.post.likedByUserIds.includes(this.userid); // assigns bool value for if liked
    }
  }

  onNotifyComment(valueEmitted: any){
    this.notifyComment.emit(valueEmitted);
  }
}
