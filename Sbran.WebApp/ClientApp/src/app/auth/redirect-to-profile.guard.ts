import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable()
export class RedirectToProfileGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.authService.isLoggedIn && (this.router.url === '/login' || this.router.url === '/')) {
      const profileId = this.authService.profileId$.getValue();
      const employeeId = this.authService.employeeId$.getValue();

      let redirectToThisUrl = `profile/${profileId}/employee/${employeeId}`;
      this.router.navigateByUrl(redirectToThisUrl);
    }

    return true;
  }
}
