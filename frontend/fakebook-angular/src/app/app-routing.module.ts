import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NewsfeedComponent } from './component/newsfeed/newsfeed.component';
import { UserProfileViewComponent } from './component/user-profile-view/user-profile-view.component';
import { AboutUsViewComponent } from './component/about-us-view/about-us-view.component';
import { OktaCallbackComponent } from '@okta/okta-angular';

const routes: Routes = [
  { path: 'newsfeed/:id', component: NewsfeedComponent},
  { path: 'newsfeed', component: NewsfeedComponent},
  { path: 'user/:id', component: UserProfileViewComponent},
  { path: 'about' , component: AboutUsViewComponent},
  { path: 'login/callback', component: OktaCallbackComponent},
  { path: 'logout', component: OktaCallbackComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), HttpClientModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
