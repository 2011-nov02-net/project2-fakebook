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
export class FollowService {
  constructor(private http: HttpClient) { }
  url = 'http://2011-project2-fakebook.azurewebsites.net/api/';

  follow(follower: User, followee: User): any {
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post(`${this.url}User/${follower}/follow/${followee}`, httpOptions)
      .toPromise().then(res => console.log(JSON.stringify(res)));
  }

  unfollow(follower: User, followee: User): any {
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post(`${this.url}User/${follower}/unfollow/${followee}`, httpOptions)
      .toPromise().then(res => console.log(JSON.stringify(res)));
  }
}
