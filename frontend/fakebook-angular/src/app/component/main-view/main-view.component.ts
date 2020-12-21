import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrls: ['./main-view.component.css']
})
export class MainViewComponent implements OnInit {
  isAuthenticated: boolean | undefined;

  constructor(private oktaAuth: AuthService) { }

  async ngOnInit() {
    this.isAuthenticated = await this.oktaAuth.isAuthenticated;
    }

}
