import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'; // getting the id number
import { Post } from 'src/app/model/post';
import { User } from 'src/app/model/user';
import { UserService } from '../../service/user.service';
import { FollowService } from '../../service/follow.service';

@Component({
  selector: 'app-user-profile-view',
  providers: [UserService, FollowService],
  templateUrl: './user-profile-view.component.html',
  styleUrls: ['./user-profile-view.component.css']
})
export class UserProfileViewComponent implements OnInit {
  user : User | undefined;
  selfUser: User | undefined;
  posts : Post[] | undefined;
  followStatus: boolean = false;

  constructor(private userService : UserService,
    private followService: FollowService, 
    private route: ActivatedRoute, // getting the id # in route
    ) { }

  ngOnInit() {
    this.getUser()

  }
  getUser(): void {
    let tempId = ""; //the only way i could declare a variable that may accept a null value in thefuture

    this.userService.getUserProfile()
      .subscribe(user => this.selfUser = user);

    // get the id number from the route
    if(this.route.snapshot.paramMap.get('id')!=null)  {
      tempId += this.route.snapshot.paramMap.get('id');

      const id = tempId;

      this.userService.getUser(id)
        .subscribe(user => this.user = user);
      this.userService.getPosts(id)
        .subscribe(posts => this.posts = posts)
      this.userService.getUser(id)
        .subscribe(user => this.userService.getUserProfile()
        .subscribe(selfUser => this.followStatus = (this.followService.getFollowStatus(selfUser, user)))); // nested getuserprofile so we can use the response to determine if self user is in the list of followers for the current user
    }
    else {
      this.userService.getUserProfile()
        .subscribe(user => this.user = user);
      this.userService.getPostsNoArg()
        .subscribe(posts => this.posts = posts);
    }

  }

  followUser(): any {
    if(this.user != undefined && this.selfUser != undefined){
      if(this.followStatus) {
        console.log("UNFOLLOW")
        this.followService.unfollow(this.selfUser, this.user);
        this.followStatus = false;
      }
      else {
        console.log("FOLLOW")
        this.followService.follow(this.selfUser, this.user);
        this.followStatus = true;
      }
    }
  }
}
