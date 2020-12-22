import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
// import { EventEmitter } from 'events';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';
import { newPost } from '../../model/newpost';
import { Post } from '../../model/post';
import { PostService } from '../../service/post.service';
import { UploadService } from '../../service/upload.service';

@Component({
  selector: 'app-new-post-form',
  providers: [PostService, UserService],
  templateUrl: './new-post-form.component.html',
  styleUrls: ['./new-post-form.component.css'],
})
export class NewPostFormComponent implements OnInit {
  submitted = false;
  file: File | null = null;
  imageSource = '';
  
  @Input() user: User | null = null;
  
  @Output() notify: EventEmitter<string> = new EventEmitter<string>();

  constructor(
    private uploadService: UploadService,
    private httpPost: PostService,
    private router: Router,
    private userService: UserService
  ) {}


  ngOnInit(): void {
    //this.getUser();
  }
  newPost = new newPost('', undefined, ''); // we'll initialize user id at onsubmit

  onSubmit() {
    if (this.file) {
      let content = this.newPost.content;
      let promise = this.save();
      if(promise) {
        promise.then((res) => {
          this.newPost.content = content;
          this.newPost.pictureUrl = res.path;
          this.newPost.userId = this.user?.id;
          this.submitted = true;
          this.httpPost.create(this.newPost);
          this.newPost.content = '';
        }).then(res => { return this.notify.emit("test value from child")});;
      }
    } else {
      this.newPost.userId = this.user?.id;
      this.submitted = true;
      this.httpPost.create(this.newPost).then(res => { return this.notify.emit("test value from child")});
    }

  }

  getUser() {
    this.userService
      .getUserProfile() // gets the user
      .subscribe((gotuser) => (this.user = gotuser));
  }

  getUserId() {
    return this.user?.id;
  }

  save(): Promise<{ path: string, userId: number }> | null {
    if (this.file && this.user) {
      const formData = new FormData();

      formData.append(this.file.name, this.file);
      formData.append('userId', `${this.user.id}`);
      return this.uploadService.upload(formData);
    }

    return null;
  }

  fileSelect(event: any): void {
    if (event.target.files[0]) {
      this.file = event.target.files[0];
    }
  }
}
