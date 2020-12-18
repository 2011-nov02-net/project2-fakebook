import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'; // getting the id number
import { Post } from 'src/app/model/post';
import { User } from 'src/app/model/user';
import {  UserService } from '../../service/user.service';

@Component({
  selector: 'app-user-profile-view',
  providers: [UserService],
  templateUrl: './user-profile-view.component.html',
  styleUrls: ['./user-profile-view.component.css']
})
export class UserProfileViewComponent implements OnInit {
  user : User | undefined; // our profile
  posts : Post[] | undefined; // our users profile

  constructor(private httpService : UserService, 
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

    this.httpService.getUser(id)
      .subscribe(user => this.user = user);

    this.httpService.getPosts(id)
      .subscribe(posts => this.posts = posts)

  }
}
