import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  @Output() cancleStatus = new EventEmitter();
  constructor(private auth: AuthService) { }

  ngOnInit() {

  }

  RegisterClick() {
    this.auth.register(this.model).subscribe(() => {
        console.log('Registration done succefully...');
    }, error => {
      console.log(error);
    });
  }
  cancleClick() {
      this.cancleStatus.emit(false);
  }

}
