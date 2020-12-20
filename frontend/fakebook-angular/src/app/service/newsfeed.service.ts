import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OktaAuthService } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';
import { Post } from '../model/post';
import { Observable } from 'rxjs';

@Injectable()
export class NewsfeedService {
  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }
  url = `${environment.baseUrl}/api/Newsfeed`;

  getPosts(id:string | null): Observable<Post[]>{
    const accessToken = this.oktaAuth.getAccessToken();
    const headers = {
      Authorization: 'Bearer ' + accessToken,
      Accept: 'application/json',
    };
    return this.http.get<Post[]>(`${this.url}/${id}`, { headers });
  }
}