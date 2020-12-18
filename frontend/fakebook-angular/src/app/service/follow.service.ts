import { Injectable } from '@angular/core';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class FollowService {
  constructor() { }

  follow(follower: User, followee: User): boolean {
    return false;
  }

  unfollow(follower: User, followee: User): boolean {
    return false;
  }
}
