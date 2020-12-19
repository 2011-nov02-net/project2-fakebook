import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router'
import { FormsModule  } from '@angular/forms';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';
import {newPost} from '../../model/newpost'
import {PostService} from '../../service/post.service'
import { NewsfeedComponent } from '../newsfeed/newsfeed.component';
@Component({
  selector: 'app-new-post-form',
  providers: [PostService , UserService],
  templateUrl: './new-post-form.component.html',
  styleUrls: ['./new-post-form.component.css']
})
export class NewPostFormComponent implements OnInit {
  submitted = false;
  constructor( private httpPost: PostService, private route: ActivatedRoute, private userService: UserService) { }
  user = this.getUser();
  ngOnInit(): void {
    //this.mygroup = new FormGroup()
  }
  newPost = new newPost('',  this.user, '')

  onSubmit() {
        this.submitted=true;
        console.log(newPost)
        //this.httpPost.create(this.newPost)
  }
  getCurrentModel() { 
    return JSON.stringify(this.newPost); 
  }
  getUser(): User | undefined {
    let id = "";

    if(this.route.snapshot.paramMap.get('id') != null)  {
      id += (this.route.snapshot.paramMap.get('id'));
    }

    if( id != null) { 
      this.userService.getUser(id).toPromise().then(user => this.user = user)
    }
    return undefined;
  }
}
