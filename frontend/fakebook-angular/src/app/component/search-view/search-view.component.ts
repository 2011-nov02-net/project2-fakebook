import { NgIf } from '@angular/common';
import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router, RoutesRecognized } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';
import { filter } from 'rxjs/operators';


@Component({
  selector: 'app-search-view',
  templateUrl: './search-view.component.html',
  styleUrls: ['./search-view.component.css']
})
export class SearchViewComponent implements OnInit {
  users : User[] | undefined; // our profile
  currentRoute: string = '';

  constructor(private httpService : UserService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.getUser();
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)).subscribe((res) => {
        this.getUser();
      });
  }
  
  ProfileSubmit(id: number)
  {
    this.router.navigateByUrl( `user/${id}`)
  }
  getUser(): void {
    let tempId = ""; //the only way i could declare a variable that may accept a null value in teh future
    
    // get the id number from the route
    if(this.route.snapshot.paramMap.get('name')!=null)  {
      tempId += this.route.snapshot.paramMap.get('name')
    }
    this.httpService.searchUser(tempId).then(res => this.users = res);
  }
}
