import { Component, OnInit } from '@angular/core';

import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public auth: AuthService,private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.auth.login(this.model).subscribe(next => {
      this.alertify.success('Login Successfull');
    }, err => {
      this.alertify.error(err);
    });
  }
  loggedIn() {

    return this.auth.loggedIn();
  }
  logout() {

    localStorage.removeItem('token');
    this.alertify.message('Logged-out Successfully');
  }
}
