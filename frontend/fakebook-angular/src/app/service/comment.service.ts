import { Injectable } from '@angular/core';

import { Comment } from '../model/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  constructor() { }

  create(comment: Comment): boolean {
    return false;
  }

  getById(id: number): Comment | undefined {
    return undefined;
  }

  getComments(count: number): Comment[] {
    return [];
  }

  getCommentsByUserId(userId: number): Comment[] {
    return [];
  }

  getCommentsByPostId(postId: number): Comment[] {
    return [];
  }

  update(comment: Comment): boolean {
    return false;
  }

  delete(commentId: number): boolean {
    return false;
  }
}
