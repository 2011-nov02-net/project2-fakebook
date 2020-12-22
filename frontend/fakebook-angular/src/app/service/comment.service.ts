import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { Comment } from '../model/comment';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  url = `${environment.baseUrl}/api/Posts/`;

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

    const targetUrl = `${this.url}${comment.postId}/Comment`;

    return this.http
      .post<Comment>(
        targetUrl,
        comment,
        { headers }
      )
      .toPromise();
  }

  delete(comment: Comment): Promise<number> {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: `Bearer ${accessToken}`,
      Accept: 'application/json'
    };

    const targetUrl = `${this.url}${comment.id}/Comment`;

    return this.http.delete<number>(
      targetUrl,
      { headers }
    ).toPromise();
  }
}
