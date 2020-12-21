import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrls: ['./main-view.component.css']
})
export class MainViewComponent implements OnInit {
  constructor(
    private oktaAuth: AuthService,
    private router: Router
  ) { }

  async ngOnInit() {
    let isAuthenticated = await this.oktaAuth.isAuthenticated;
    if(isAuthenticated) {
      // redirect the user to the newsfeed page
      this.router.navigateByUrl('newsfeed', { skipLocationChange: false });
    }
  }
}
