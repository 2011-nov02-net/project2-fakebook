import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NewsfeedComponent } from './component/newsfeed/newsfeed.component';
import { UserProfileViewComponent } from './component/user-profile-view/user-profile-view.component';

const routes: Routes = [
  { path: '', redirectTo: '/newsfeed', pathMatch: 'full' },
  { path: 'user/:id', component: UserProfileViewComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes), HttpClientModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
