import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  @Output() cancleStatus = new EventEmitter();
  constructor(private auth: AuthService,private alertify: AlertifyService) { }

  ngOnInit() {

  }

  RegisterClick() {
    this.auth.register(this.model).subscribe(() => {
      this.alertify.success('Registration done succefully...');
    }, error => {
      this.alertify.error(error);
    });
  }
  cancleClick() {
      this.cancleStatus.emit(false);
  }

}
