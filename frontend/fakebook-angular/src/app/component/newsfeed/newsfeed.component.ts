import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../../service/config.service';
import { Post } from '../../model/post';

@Component({
  selector: 'app-newsfeed',
  providers: [ConfigService],
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css']
})
export class NewsfeedComponent implements OnInit {
  posts: any[] = [];

  constructor(private configService: ConfigService) { }

  ngOnInit(){
    this.getPosts();
  }

  getPosts(): void {
    this.configService.getPosts()
      .subscribe(posts => console.log(posts));
  }
}
