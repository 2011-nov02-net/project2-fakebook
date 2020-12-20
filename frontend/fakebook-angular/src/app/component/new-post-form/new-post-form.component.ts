import { Component, Injectable, Input, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router'
import { FormsModule  } from '@angular/forms';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';
import {newPost} from '../../model/newpost'
import {PostService} from '../../service/post.service'
import { NewsfeedComponent } from '../newsfeed/newsfeed.component';
import { inject } from '@angular/core/testing';
@Component({
  selector: 'app-new-post-form',
  providers: [PostService , UserService],
  templateUrl: './new-post-form.component.html',
  styleUrls: ['./new-post-form.component.css']
})
@Injectable()
export class NewPostFormComponent implements OnInit {
  submitted = false;
  constructor( private httpPost: PostService, private route: ActivatedRoute, private userService: UserService) { }

  userid = this.getUserId();
  @Input() user: User | undefined;
  ngOnInit(): void {
    //this.mygroup = new FormGroup()
  }
  newPost = new newPost('',  this.userid, '')

  onSubmit() {
        this.submitted=true;
        this.httpPost.create(this.newPost)
        //this.httpPost.create(this.newPost)
  }
  getCurrentModel() { 
    return JSON.stringify(this.newPost); 
  }
  getUserId(): string | undefined {
    let id = "";

    if(this.route.snapshot.paramMap.get('id') != null)  {
      id += (this.route.snapshot.paramMap.get('id'));
      return id;
    }

    return undefined;
  }
}
