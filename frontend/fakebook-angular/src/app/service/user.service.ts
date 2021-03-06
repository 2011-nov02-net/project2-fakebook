import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/user';
import { Observable } from 'rxjs';
import { Post } from '../model/post';
import { OktaAuthService } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) {}
  url = `${environment.baseUrl}/api/`; // url of the api

  getUser(id: string | undefined): Observable<User> {
    return this.http.get<User>(`${this.url}User/${id}`);
  }

  getUserProfile(): Observable<User> {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.get<User>(`${this.url}User/profile`, { headers });
  }
  updateUserProfile(id: number, user: User): Promise<User> {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.put<User>(`${this.url}User/${id}`, user , { headers }).toPromise();

  }
  getPosts(id: string | null): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.url}User/${id}/Posts`);
  }

  getPostsNoArg(): Observable<Post[]> {
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.get<Post[]>(`${this.url}User/Posts`, { headers });
  }
  
  searchUser(name: string | null): Promise<User[]> {
    return this.http.get<User[]>(`${this.url}User/search/${name}`).toPromise();
  }
}
