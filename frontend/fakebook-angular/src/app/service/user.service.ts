import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/user';
import { Observable } from 'rxjs';
import { Post } from '../model/post';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }
  url = `${environment.baseUrl}/api/`; // url of the api

  login(email: string, password: string): void {

  }

  logout(): void {

  }

  register(user: User): void {

  }
  getUser(id:string | null): Observable<User>{
    return this.http.get<User>(`${this.url}User/${id}`);
  }
  getPosts(id:string | null): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.url}User/${id}/Posts`);
  }

}
