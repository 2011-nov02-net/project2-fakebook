import { Injectable } from '@angular/core';
import { Post } from '../model/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor() { }

  create(post: Post): boolean {
    return false;
  }

  getById(id: number): Post | undefined {
    return undefined;
  }

  getPosts(count: number): Post[] {
    return [];
  }

  getPostsByUserId(userId: number): Post[] {
    return [];
  }

  update(post: Post): boolean {
    return false;
  }

  delete(postId: number): boolean {
    return false;
  }
}
