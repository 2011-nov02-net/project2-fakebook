import { Component, Input, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service'
import { FormControl } from '@angular/forms';
@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent   {
  firstName = new FormControl('');
  lastName = new FormControl('');
  status = new FormControl('');

  modalRef!: BsModalRef ;
  profileForm: any;
  constructor(private modalService: BsModalService, private userService: UserService) {}

  @Input() user!: User;
  
  ngOnInit(): void {
  }
 
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template); 
    this.firstName.setValue(this.user.firstName);
    this.lastName.setValue(this.user.lastName);
    this.status.setValue(this.user.status);

  }
  submit() {
    this.user.firstName = this.firstName.value;
    this.user.lastName = this.lastName.value;
    this.user.status = this.status.value;
    this.userService.updateUserProfile(this.user.id, this.user)
    this.modalRef.hide()
  }
  onSubmit() {
    console.warn(this.firstName); 
    this.modalRef.hide()

  }
}