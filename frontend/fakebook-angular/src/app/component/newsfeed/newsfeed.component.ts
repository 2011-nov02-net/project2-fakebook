import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NewsfeedService } from '../../service/newsfeed.service';
import { UserService } from '../../service/user.service';
import { LikeService } from '../../service/like.service';
import { Post } from '../../model/post';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-newsfeed',
  providers: [UserService, NewsfeedService],
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css'],
})
export class NewsfeedComponent implements OnInit {
  posts: Post[] | undefined;
  user: User | null = null;
  constructor(
    private likeService: LikeService,
    private router: Router,
    private userService: UserService,
    private route: ActivatedRoute,
    private newsfeedService: NewsfeedService
  ) {}

  ngOnInit() {
    this.getUser();
  }

  /* getPosts(int: string): void {
      this.posts = this.newsfeedService.getPosts(int)
  }*/
  ProfileSubmit(id: number) {
    this.router.navigateByUrl(`user/${id}`);
  }
  getUser() {
    this.userService
      .getUserProfile() // gets the user
      .subscribe((gotuser) => (this.user = gotuser));

    this.newsfeedService
      .getPosts(null)
      .subscribe((posts) => this.posts = posts);
  }
}
