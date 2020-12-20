import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthenticated: boolean = false;

  constructor(public oktaAuth: OktaAuthService) { 
    this.oktaAuth.$authenticationState.subscribe((isAuthenticated) =>
      this.updateAuthState(isAuthenticated)
    );
  }

  updateAuthState(isAuthenticated: boolean) {
    this.isAuthenticated = isAuthenticated;
    if (isAuthenticated) {
      this.oktaAuth.getUser().then(console.log)
    }
  }

  async ngOnInit() {
    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
  }
  
  subscribeAuthStateChange(updateFn: (authState: boolean) => void) {
    this.oktaAuth.$authenticationState.subscribe((authState) => updateFn(authState));
  }

  login() {
    this.oktaAuth.signInWithRedirect();
  }

  logout(){
    this.oktaAuth.signOut();
  }
}
