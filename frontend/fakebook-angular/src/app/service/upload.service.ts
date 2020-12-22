import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class UploadService {
  constructor(private http: HttpClient) {}

  upload(formData: FormData) {
    return this.http.post<{ path: string }>(
      'https://2011-project2-fakebook.azurewebsites.net/api/Posts/UploadPicture',
      formData
    ).toPromise();
  }
}