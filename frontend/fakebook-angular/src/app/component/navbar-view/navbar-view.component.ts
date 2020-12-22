import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import {UserService} from '../../service/user.service'
import {Router} from '@angular/router'
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-navbar-view',
  providers: [AuthService],
  templateUrl: './navbar-view.component.html',
  styleUrls: ['./navbar-view.component.css']
})
export class NavbarViewComponent implements OnInit {
  isAuthenticated: boolean = false;

  searchName: string = '';
  constructor(private oktaAuth: AuthService, private httpService: UserService, private router: Router) { }

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
  onNotifySearch(name: any){
    console.log(name)
    this.router.navigate([`search/${name}`], { skipLocationChange: false })
  }
}
