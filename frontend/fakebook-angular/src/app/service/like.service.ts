import { Injectable } from '@angular/core';
import { Post } from '../model/post';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class LikeService {
  constructor() { }

  like(post: Post, user: User): boolean {
    return false;
  }

  unlike(post: Post, user: User): boolean {
    return false;
  }
}
