import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NewsfeedComponent } from './component/newsfeed/newsfeed.component';
import { PostViewComponent } from './component/post-view/post-view.component';
import { UserViewComponent } from './component/user-view/user-view.component';
import { UserProfileViewComponent } from './component/user-profile-view/user-profile-view.component';
import { ProfilePictureViewComponent } from './component/profile-picture-view/profile-picture-view.component';
import { CommentViewComponent } from './component/comment-view/comment-view.component';
import { LikeViewComponent } from './component/like-view/like-view.component';
import { UserControlsComponent } from './component/user-controls/user-controls.component';
import { NavbarViewComponent } from './component/navbar-view/navbar-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterViewComponent } from './component/footer-view/footer-view.component';
import { AboutUsViewComponent } from './component/about-us-view/about-us-view.component';

@NgModule({
  declarations: [
    AppComponent,
    NewsfeedComponent,
    PostViewComponent,
    UserViewComponent,
    UserProfileViewComponent,
    ProfilePictureViewComponent,
    CommentViewComponent,
    LikeViewComponent,
    UserControlsComponent,
    NavbarViewComponent,
    FooterViewComponent,
    AboutUsViewComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
