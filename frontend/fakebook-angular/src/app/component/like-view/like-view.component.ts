import { Component, Input, OnInit } from '@angular/core';
import { LikeService } from 'src/app/service/like.service';

@Component({
  selector: 'app-like-view',
  templateUrl: './like-view.component.html',
  styleUrls: ['./like-view.component.css']
})
export class LikeViewComponent implements OnInit {
  @Input() count!: number;
  @Input() postId!: number;
  @Input() liked!: boolean;

  constructor(
    private likeService: LikeService,
  ) { }

  ngOnInit(): void { }

  submit(liked: boolean): void {
    if(liked) {
      this.likeService.unlike(this.postId);
      this.liked = !liked;
      this.count--;
    } 
    else {
      this.likeService.like(this.postId);
      this.liked = !liked;
      this.count++;
    }
  }
}
