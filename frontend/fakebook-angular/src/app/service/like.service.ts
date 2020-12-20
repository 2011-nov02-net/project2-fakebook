import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Post } from '../model/post';
import { User } from '../model/user';
import { Like } from '../model/like';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { OktaAuthService } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LikeService {
  url = `${environment.baseUrl}/api/`;
  
  constructor(
    private http: HttpClient,
    private oktaAuth: OktaAuthService
  ) { }
  
  like(postId: number): any {
    const accessToken = this.oktaAuth.getAccessToken();
    let httpOptions = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${accessToken}`,
        'Content-Type': 'application/json'
      })
    };

    return this.http
      .post(`${this.url}Posts/${postId}/like/`, null, httpOptions)
      .toPromise()
      .then(res => console.log(JSON.stringify(res)));
  }

  unlike(postId: number): any {
    const accessToken = this.oktaAuth.getAccessToken();
    let httpOptions = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${accessToken}`,
        'Content-Type': 'application/json'
      })
    };

    return this.http
      .post(`${this.url}Posts/${postId}/unlike/`, null, httpOptions)
      .toPromise()
      .then(res => console.log(JSON.stringify(res)));
  }

  handleError(error: any) {
    console.error('An error occurred', error);
    return throwError(error.message || "Like Error");
 }
}
