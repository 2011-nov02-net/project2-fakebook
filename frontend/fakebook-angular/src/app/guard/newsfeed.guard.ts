// import { Injectable } from '@angular/core';
// import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
// import { AuthService } from '../service/auth.service';

// @Injectable({
//   providedIn: 'root'
// })
// export class NewsfeedGuard implements CanActivate {
//   constructor(private authService: AuthService, private router: Router) {}
//   canActivate(
//     next: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot): true|UrlTree {
//     const url: string = state.url;

//     return this.checkLogin(url);
//   }

//   checkLogin(url: string): true|UrlTree {
//     console.log(url);
//     if (this.authService.isAuthenticated) { return true; }

//     // Redirect to the login page
//     return this.router.parseUrl('');
//   }
// }
