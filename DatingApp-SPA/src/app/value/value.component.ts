import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {

  values: any;
  url = 'http://localhost:5000/api/values';
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getvalues();
  }

  getvalues()
  {
      this.http.get( this.url ).subscribe(res => {
            this.values = res;
      }, err => {
        console.log(err);
      });
  }

}

