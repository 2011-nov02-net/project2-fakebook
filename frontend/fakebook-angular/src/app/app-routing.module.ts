import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NewsfeedComponent } from './component/newsfeed/newsfeed.component';
import { UserProfileViewComponent } from './component/user-profile-view/user-profile-view.component';
import { AboutUsViewComponent } from './component/about-us-view/about-us-view.component';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { SearchViewComponent } from './component/search-view/search-view.component';
import { MainViewComponent } from './component/main-view/main-view.component';

const routes: Routes = [
  { path: 'newsfeed', component: NewsfeedComponent },
  { path: 'user/:id', component: UserProfileViewComponent },
  { path: 'user', component: UserProfileViewComponent },
  { path: 'about', component: AboutUsViewComponent },
  { path: 'login/callback', component: OktaCallbackComponent },
  { path: 'logout', component: OktaCallbackComponent },
  { path: 'search/:name', component: SearchViewComponent },
  { path: '', component: MainViewComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), HttpClientModule],
  exports: [RouterModule],
})
export class AppRoutingModule {}
