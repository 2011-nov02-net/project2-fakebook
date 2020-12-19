import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';

@Component({
  selector: 'app-navbar-view',
  templateUrl: './navbar-view.component.html',
  styleUrls: ['./navbar-view.component.css']
})
export class NavbarViewComponent implements OnInit {
  isAuthenticated: boolean = false;

  constructor(public oktaAuth: OktaAuthService) { 
    this.oktaAuth.$authenticationState.subscribe((isAuthenticated) =>
      this.updateAuthState(this.isAuthenticated)
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

  login() {
    this.oktaAuth.signInWithRedirect();
  }
}
