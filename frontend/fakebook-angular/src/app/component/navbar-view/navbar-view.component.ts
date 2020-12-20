import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import {UserService} from '../../service/user.service'
import {Router} from '@angular/router'
import { FormControl } from '@angular/forms';
@Component({
  selector: 'app-navbar-view',
  templateUrl: './navbar-view.component.html',
  styleUrls: ['./navbar-view.component.css']
})
export class NavbarViewComponent implements OnInit {
  isAuthenticated: boolean = false;
  searchName = new FormControl('');
  constructor(private oktaAuth: AuthService, private httpService: UserService, private router: Router) { }

  async ngOnInit() {
    this.oktaAuth.subscribeAuthStateChange((authState: boolean) => {
      this.isAuthenticated = authState;
    });
  }
  
  login() {
    this.oktaAuth.login();
  }
  onSubmit() {
    this.router.navigateByUrl(`search/${this.searchName.value}`, { skipLocationChange: false });
  } 
  logout() {
      this.oktaAuth.logout();
  }
}
