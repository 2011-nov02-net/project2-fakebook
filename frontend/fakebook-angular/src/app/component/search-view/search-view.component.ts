import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-search-view',
  templateUrl: './search-view.component.html',
  styleUrls: ['./search-view.component.css']
})
export class SearchViewComponent implements OnInit {
  users : User[] | undefined; // our profile

  constructor(private httpService : UserService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.getUser();
  }
  // reroutes the profile button on any view to the user page
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
    
    this.httpService.searchUser(tempId)
      .subscribe(user => this.users = user);
    
  }
}
