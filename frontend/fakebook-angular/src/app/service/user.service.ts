import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/user';
import { Observable } from 'rxjs';
import { Post } from '../model/post';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }
  url = 'https://2011-project2-fakebook.azurewebsites.net/api/'; // url of the api

  login(email: string, password: string): void {

  }

  logout(): void {

  }

  register(user: User): void {

  }
  getUser(id:string | undefined): Observable<User>{
    return this.http.get<User>(`${this.url}User/${id}`);
  }
  getUserProfile(): Observable<User>{
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.get<User>(`${this.url}User/profile`, {headers})
  }
  getPosts(id:string | null): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.url}User/${id}/Posts`)
  }
  searchUser(name:string | null): Observable<User[]> {
    return this.http.get<User[]>(`${this.url}User/search/${name}`)
  }

}
