import { Injectable } from '@angular/core';
import { CanActivate,  Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { AlertPromise } from 'selenium-webdriver';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router, private alertify : AlertifyService) {}
  canActivate():  boolean {
    if (this.auth.loggedIn()) {
      return true;
    } else {
      this.alertify.error('Please login with valid credentials then try again !!..');
      this.router.navigate(['/home']);
    }
  return false;
  }
}
