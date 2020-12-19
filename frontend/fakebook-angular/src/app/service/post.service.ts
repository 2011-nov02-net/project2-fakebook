import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../model/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private http: HttpClient) { }
  url = 'http://2011-project2-fakebook.azurewebsites.net/api/';

  create(post: Post): Observable<Post>{
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<Post>(`api/posts`, post, httpOptions);
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
