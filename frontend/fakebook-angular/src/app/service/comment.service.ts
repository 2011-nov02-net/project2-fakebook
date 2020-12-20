import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { Comment } from '../model/comment';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  url = `${environment.baseUrl}/api/posts/`;

  constructor(
    private http: HttpClient,
    private oktaAuth: OktaAuthService
  ) { }

  create(comment: Comment): Promise<Comment> {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: `Bearer ${accessToken}`,
      Accept: 'application/json'
    };

    return this.http
      .post<Comment>(
        `${this.url}${comment.postId}/comment`,
        comment,
        { headers }
      )
      .toPromise();
  }
}
