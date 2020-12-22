import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NewsfeedService } from '../../service/newsfeed.service';
import { UserService } from '../../service/user.service';
import { LikeService } from '../../service/like.service';
import { Post } from '../../model/post';
import { User } from 'src/app/model/user';
import { newPost } from 'src/app/model/newpost';
import { trigger, transition, style, animate, query, stagger, state } from '@angular/animations';

@Component({
  selector: 'app-newsfeed',
  providers: [UserService, NewsfeedService],
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css'],
  animations: [
    // the fade-in/fade-out animation.
    trigger('simpleFadeAnimation', [

      // the "in" style determines the "resting" state of the element when it is visible.
      state('in', style({opacity: 1})),

      // fade in when created. this could also be written as transition('void => *')
      transition(':enter', [
        style({opacity: 0}),
        animate(600 )
      ]),

      // fade out when destroyed. this could also be written as transition('void => *')
      transition(':leave',
        animate(600, style({opacity: 0})))
    ])
  ]
})

export class NewsfeedComponent implements OnInit  {
  state = 'out';
  
  ngAfterViewInit() {
    this.state = this.state === 'in' ? 'out' : 'in';
  }

  
  posts: Post[] =[];
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
  ProfileSubmit(id: number) {
    this.router.navigateByUrl(`user/${id}`);
  }
  getUser() {
      this.userService.getUserProfile() // gets the user 
          .subscribe(gotuser => this.user = gotuser)
    
    this.newsfeedService.getPosts(null)
        .subscribe((posts) => this.posts = this.posts.length ? [] :posts);
  }

  onNotifyClicked(valueEmitted: any){
    console.log("valueEmitted");
    this.newsfeedService.getPosts(null)
      .subscribe(posts => this.posts = posts)
  }
}
