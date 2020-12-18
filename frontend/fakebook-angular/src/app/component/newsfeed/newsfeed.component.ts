import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NewsfeedService } from '../../service/newsfeed.service';
import { UserService } from '../../service/user.service';
import { Post } from '../../model/post';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-newsfeed',
  providers: [UserService, NewsfeedService],
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css']
})
export class NewsfeedComponent implements OnInit {
  posts: Post[] | null = null;

  constructor(private userService : UserService, private route: ActivatedRoute, private newsfeedService: NewsfeedService) { }

  ngOnInit(){
    this.getUser();
  }

  getPosts(int: number): void {
      this.newsfeedService.getPosts(int)
        .subscribe(posts => this.posts = posts);
  }

  getUser() {
    let id = "";

    if(this.route.snapshot.paramMap.get('id') != null)  {
      id += (this.route.snapshot.paramMap.get('id'));
    }

    if( id != null) { 
      this.userService.getUser(id).toPromise()
      .then(user => this.getPosts(user.id))
    }
  }
}
