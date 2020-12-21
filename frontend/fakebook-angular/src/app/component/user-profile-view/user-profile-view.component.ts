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
  user : User | undefined; // our profile
  selfUser: User | undefined;
  posts : Post[] | undefined; // our users profile

  constructor(private userService : UserService,
    private followService: FollowService, 
    private route: ActivatedRoute, // getting the id # in route
    ) { }

  ngOnInit(): void {
    this.getUser();

  }
  getUser(): void {
    let tempId = ""; //the only way i could declare a variable that may accept a null value in teh future
    
    // get the id number from the route
    if(this.route.snapshot.paramMap.get('id')!=null)  {
      tempId += this.route.snapshot.paramMap.get('id');

      const id = tempId;

      this.userService.getUser(id)
        .subscribe(user => this.user = user);
      this.userService.getPosts(id)
        .subscribe(posts => this.posts = posts)
    }
    else {
      this.userService.getUserProfile()
        .subscribe(user => this.user = user);
      this.userService.getPostsNoArg()
        .subscribe(posts => this.posts = posts);
    }

    this.userService.getUserProfile()
      .subscribe(user => this.selfUser = user);

  }

  followUser(): any {
    if(this.user != undefined && this.selfUser != undefined){
      if(this.user.followers.find(users => this.user?.id == this.selfUser?.id) != undefined) {
        console.log("UNFOLLOW")
        this.followService.unfollow(this.user);
      }
      else {
        console.log("FOLLOW")
        this.followService.follow(this.user);
      }
    }
  }
}
