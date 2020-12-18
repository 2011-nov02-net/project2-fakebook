import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../model/post';

@Injectable()
export class NewsfeedService {
  constructor(private http: HttpClient) { }
  url = 'http://2011-project2-fakebook.azurewebsites.net/api/';
  
  getPosts(id: number | null): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.url}Newsfeed/${id}`);
  }
}