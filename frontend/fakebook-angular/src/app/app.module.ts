import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { OktaViewComponent } from './component/okta-view/okta-view.component';
import { OktaAuthModule,
         OKTA_CONFIG }
         from '@okta/okta-angular';
import { NewPostFormComponent } from './component/new-post-form/new-post-form.component';
import { SearchViewComponent } from './component/search-view/search-view.component';
import { CommentFormComponent } from './component/comment-form/comment-form.component';

const config = {
  issuer: 'https://dev-2137068.okta.com/oauth2/default',
  pkce: true,
  clientId: '0oa2rgza5gsrQvaBz5d6',
  redirectUri: `${window.location.origin}/login/callback`,
  scopes: ['openid'],
  postLogoutRedirectUri: window.location.origin
}

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
    AboutUsViewComponent,
    OktaViewComponent,
    CommentFormComponent
    NewPostFormComponent,
    SearchViewComponent
    
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    OktaAuthModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [{ provide: OKTA_CONFIG, useValue: config }],
  bootstrap: [AppComponent]
})
export class AppModule { }
