import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NewsfeedService } from '../../service/newsfeed.service';
import { UserService } from '../../service/user.service';
import { LikeService } from '../../service/like.service';
import { Post } from '../../model/post';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-newsfeed',
  providers: [UserService, NewsfeedService],
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css']
})
export class NewsfeedComponent implements OnInit {
  posts: Post[] | undefined;
  user: User | undefined;
  constructor(private likeService: LikeService, private userService: UserService, private route: ActivatedRoute, private newsfeedService: NewsfeedService) { }

  ngOnInit(){
    this.getUser();

  }

 /* getPosts(int: string): void {
      this.posts = this.newsfeedService.getPosts(int)
  }*/

  getUser() {
    let id = "";

    if(this.route.snapshot.paramMap.get('id') != null)  {
      id += (this.route.snapshot.paramMap.get('id'));
    }

    if( id != null) { 
      this.userService.getUser(id)
        .subscribe(gotuser => this.user = gotuser)
    }
    
    this.newsfeedService.getPosts(id)
        .subscribe(posts => this.posts = posts)
  }
}
