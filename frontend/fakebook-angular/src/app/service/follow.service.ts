import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../model/user';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class FollowService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }
  url = `${environment.baseUrl}/api/User`;

  follow(follower: User, followee: User): any {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.post(`${this.url}/${follower}/follow/${followee}`, headers)
      .toPromise().then(res => console.log(JSON.stringify(res)));
  }

  unfollow(follower: User, followee: User): any {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.post(`${this.url}/${follower}/unfollow/${followee}`, headers)
      .toPromise().then(res => console.log(JSON.stringify(res)));
  }
}
