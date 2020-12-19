import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../model/post';
import { OktaAuthService } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }
  url = `${environment.baseUrl}/api/Posts`;

  create(post: Post): Observable<Post>{
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.post<Post>(`${this.url}`, post, { headers });
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
