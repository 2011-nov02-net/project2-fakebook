import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '../model/post';
import { OktaAuthService } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';
import { newPost } from '../model/newpost'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }
  url = `${environment.baseUrl}/api/Posts`;

  create(post: newPost): Promise<newPost> {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.post<newPost>(`${this.url}`, post, { headers }).toPromise();
  }

  getById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.url}/${id}`);
  }

  delete(postId: number): Promise<number> {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };

    return this.http.delete<number>(
      `${this.url}/${postId}`, { headers }
    ).toPromise();
  }
}
