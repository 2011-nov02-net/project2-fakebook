import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';
import { LikeService } from 'src/app/service/like.service';
import { Post } from '../../model/post';
import { Comment } from 'src/app/model/comment';

@Component({
  selector: 'app-post-view',
  templateUrl: './post-view.component.html',
  styleUrls: ['./post-view.component.css']
})
export class PostViewComponent implements OnInit {
  @Input() post: Post | null = null;
  @Input() userid: number=0;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void { this.userLiked()}

  getUserId(): number {
    let id = -1;

    let idStr = this.route.snapshot.paramMap.get('id');
    if (idStr) {
      id = +idStr;
    }

    return id;
  }

  userLiked() {
    if (this.post) {
      console.log(this.userid)
        this.post.liked = this.post.likedByUserIds.includes(this.userid) // assigns bool value for if liked
      }
  }
}
