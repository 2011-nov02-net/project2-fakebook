import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { UserService } from '../../service/user.service'
import { Router } from '@angular/router'
import { FormControl } from '@angular/forms';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css']
})
export class SearchFormComponent implements OnInit {
  searchName = new FormControl('');

  constructor(private userService: UserService) { }

  @Output() notifySearch: EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {
  }

  onSubmit() {
    if(this.searchName){
      this.userService.searchUser(this.searchName.value).then(res => this.notifySearch.emit(this.searchName.value));
    }
  }
}
