import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registertoggle = false;
  values: any;
  url = 'http://localhost:5000/api/values';
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getvalues();
  }

  RegisterClick() {
  this.registertoggle = true;
  }
  getvalues() {
      this.http.get( this.url ).subscribe(res => {
            this.values = res;
      }, err => {
        console.log(err);
      });
  }
  cancleEventFromChild(status: boolean) {
    this.registertoggle = status;
  }
}
