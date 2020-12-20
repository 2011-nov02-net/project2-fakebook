import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/service/user.service';
import { newPost } from '../../model/newpost'
import { PostService } from '../../service/post.service'

@Component({
  selector: 'app-new-post-form',
  providers: [PostService, UserService],
  templateUrl: './new-post-form.component.html',
  styleUrls: ['./new-post-form.component.css']
})
export class NewPostFormComponent implements OnInit {
  submitted = false;
  user = this.getUser();
  newPost = new newPost('', this.user, '')

  constructor(
    private httpPost: PostService, 
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.submitted = true;
    this.httpPost.create(this.newPost)
    //this.httpPost.create(this.newPost)
  }

  getCurrentModel() {
    return JSON.stringify(this.newPost);
  }
  
  getUser(): string | undefined {
    let id = "";

    if (this.route.snapshot.paramMap.get('id') != null) {
      id += (this.route.snapshot.paramMap.get('id'));
      return id;
    }

    return undefined;
  }
}
