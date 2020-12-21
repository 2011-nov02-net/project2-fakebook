import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../model/post';
import { OktaAuthService } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';
import {newPost} from '../model/newpost'

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }
  url = `https://2011-project2-fakebook.azurewebsites.net/api/Posts`;

  create(post: newPost): Promise<newPost>{
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.post<newPost>(`${this.url}`, post, { headers }).toPromise();;
  }

  getById(id: number): Post | undefined {
    return undefined;
  }

  getPosts(count: number): Post[] {
    return [];
  }

  getPostsByUserId(userId: number): Post[] {
    return [];
  }

  update(post: Post): boolean {
    return false;
  }

  delete(postId: number): boolean {
    return false;
  }
}
