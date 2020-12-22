import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../model/user';
import { OktaAuthService } from '@okta/okta-angular';
import { FnParam } from '@angular/compiler/src/output/output_ast';

@Injectable({
  providedIn: 'root',
})
export class FollowService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) {}
  url = `${environment.baseUrl}/api`;

  follow(follower: User, followee: User): any {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
      'Content-Type': 'application/json',
    };
    return this.http
      .post(`${this.url}/User/${follower.id}/follow/${followee.id}`, null, {
        headers,
      })
      .toPromise();
  }

  unfollow(follower: User, followee: User): any {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http
      .post(`${this.url}/User/${follower.id}/unfollow/${followee.id}`, null, {
        headers,
      })
      .toPromise()
      .then((res) => console.log(JSON.stringify(res)));
  }

  getFollowStatus(follower: User, followee: User): boolean {
    return followee.followers.some((user) => user.id == follower.id);
  }
}
