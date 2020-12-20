import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-navbar-view',
  templateUrl: './navbar-view.component.html',
  styleUrls: ['./navbar-view.component.css']
})
export class NavbarViewComponent implements OnInit {
  isAuthenticated: boolean = false;

  constructor(private oktaAuth: AuthService) { }

  async ngOnInit() {
    this.oktaAuth.subscribeAuthStateChange((authState: boolean) => {
      this.isAuthenticated = authState;
    });
  }
  
  login() {
    this.oktaAuth.login();
  }

  logout() {
      this.oktaAuth.logout();
  }
}
