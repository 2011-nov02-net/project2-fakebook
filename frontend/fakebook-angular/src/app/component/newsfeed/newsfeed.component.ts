import { Component, OnInit } from '@angular/core';
import { NewsfeedService } from '../../service/newsfeed.service';
import { Post } from '../../model/post';

@Component({
  selector: 'app-newsfeed',
  providers: [NewsfeedService],
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css']
})
export class NewsfeedComponent implements OnInit {
  posts: Post[] | null =  null;

  constructor(private newsfeedService: NewsfeedService) { }

  ngOnInit(){
    this.getPosts();
  }

  getPosts(): void {
    this.newsfeedService.getPosts()
      .subscribe(posts => this.posts = posts);
  }
}
