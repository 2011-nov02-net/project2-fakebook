import { Injectable } from '@angular/core';

import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor() { }

  login(email: string, password: string): void {

  }

  logout(): void {

  }

  register(user: User): void {

  }
}
