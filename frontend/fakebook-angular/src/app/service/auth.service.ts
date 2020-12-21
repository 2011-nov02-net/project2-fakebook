import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { map, filter, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthenticated: boolean = false;

  constructor(public oktaAuth: OktaAuthService, public router: Router) { 
    this.oktaAuth.$authenticationState.subscribe((isAuthenticated) =>
      this.updateAuthState(isAuthenticated)
    );
  }

  updateAuthState(isAuthenticated: boolean) {
    this.isAuthenticated = isAuthenticated;
    if (isAuthenticated) {
      this.oktaAuth.getUser().then(console.log);
    }
  }

  async ngOnInit() {
    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
  }
  
  subscribeAuthStateChange(updateFn: (authState: boolean) => void) {
    this.oktaAuth.$authenticationState.subscribe((authState) => updateFn(authState));
  }

  login() {
    this.oktaAuth.signInWithRedirect({
      originalUri: 'newsfeed'
    });
  }

  logout(){
    this.oktaAuth.signOut();
    this.oktaAuth.tokenManager.clear();
  }
}
