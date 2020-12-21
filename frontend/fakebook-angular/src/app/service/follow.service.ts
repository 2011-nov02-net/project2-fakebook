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
  url = `https://2011-project2-fakebook.azurewebsites.net/api`;

  follow(follower: User, followee: User): any {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
      'Content-Type': 'application/json'
    };
    console.log(followee.firstName)
    return this.http.post(`${this.url}/User/${follower.id}/follow/${followee.id}`, null, { headers })
      .toPromise().then(res => console.log(JSON.stringify(res)));
  }

  unfollow(follower: User, followee: User): any {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.post(`${this.url}/User/${follower.id}/unfollow/${followee.id}`, null, { headers })
      .toPromise().then(res => console.log(JSON.stringify(res)));
  }
}
