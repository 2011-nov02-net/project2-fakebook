import { Component, Injectable, Input, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router'
import { FormsModule  } from '@angular/forms';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';
import {newPost} from '../../model/newpost'
import {PostService} from '../../service/post.service'
import { NewsfeedComponent } from '../newsfeed/newsfeed.component';
import { inject } from '@angular/core/testing';
import { UploadService } from '../../service/upload.service';

@Component({
  selector: 'app-new-post-form',
  providers: [PostService , UserService],
  templateUrl: './new-post-form.component.html',
  styleUrls: ['./new-post-form.component.css']
})
export class NewPostFormComponent implements OnInit {
  submitted = false;
  filename = '';
  imageSource = '';

  constructor(private uploadService: UploadService, private httpPost: PostService, private route: ActivatedRoute, private userService: UserService) { }

  @Input() user: User | null=null;
  ngOnInit(): void {
    //this.getUser();
  }
  newPost = new newPost('',  undefined, '') // we'll initialize user id at onsubmit

  onSubmit() {
       this.newPost.userId = this.user?.id;
        this.submitted=true;
        this.httpPost.create(this.newPost)
  }
  getUser() {
    this.userService.getUserProfile() // gets the user 
        .subscribe(gotuser => this.user = gotuser)
  }
  getUserId() {
    return this.user?.id;
  }
  save(files: any) {
    const formData = new FormData();

    if (files[0]) {
      formData.append(files[0].name, files[0]);
    }

    this.uploadService
      .upload(formData)
      .subscribe(({ path }) => (this.imageSource = path));
  }

  setFilename(files: any) {
    if (files[0]) {
      this.filename = files[0].name;
    }
  }
}
