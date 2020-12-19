import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Post } from '../model/post';
import { User } from '../model/user';
import { Like } from '../model/like';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LikeService {
  constructor(private http: HttpClient) { }
  url = 'http://2011-project2-fakebook.azurewebsites.net/api/';

  like(post: Post, user: User): any {
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post(`${this.url}Posts/${post.id}/like/${user.id}`, httpOptions)
    .toPromise().then(res => console.log(JSON.stringify(res)));
      // .pipe(
      //   tap((newLike: Like) => console.log(`add log w/ id='${newLike}'`)),
      //   catchError(this.handleError)
      // );
  }

  unlike(post: Post, user: User): boolean {
    return false;
  }

  handleError(error: any) {
    console.error('An error occurred', error);
    return throwError(error.message || "Like Error");
 }
}
