import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ConfigService {
  constructor(private http: HttpClient) { }
  url = 'https://localhost:44347/api/';
  
  getPosts() {
    return this
            .http
            .get(`${this.url}Posts`);
  }
}