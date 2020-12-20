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
      tempId += this.route.snapshot.paramMap.get('id')
    }
    // assign this to an id
    const id = tempId;

    this.userService.getUser(id)
      .subscribe(user => this.user = user);

    this.userService.getPosts(id)
      .subscribe(posts => this.posts = posts)
  }

  followUser(): any {
    if(this.user != undefined) {
      this.followService.follow(this.user);
    }
  }
}
