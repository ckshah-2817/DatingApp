import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map} from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseURL = 'http://localhost:5000/api/auth/';
jwtHelper = new JwtHelperService();
decodedToken: any;
constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(this.baseURL + 'login', model).pipe(
      map((res: any) => {
       const userToken = res;
       if (userToken) {

          localStorage.setItem('token', userToken.token);
          this.decodedToken = this.jwtHelper.decodeToken(userToken.token);
       }

      })

  );

}

register(model: any) {
  return this.http.post(this.baseURL + 'register', model);
}

loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);

}
}
