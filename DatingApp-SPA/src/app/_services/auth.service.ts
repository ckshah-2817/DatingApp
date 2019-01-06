import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseURL = 'http://localhost:5000/api/auth/';
constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(this.baseURL + 'login', model).pipe(
      map((res: any) => {
       const userToken = res;
       if (userToken) {
          localStorage.setItem('token', userToken.token);
       }

      })

  );

}

register(model:any){
  return this.http.post(this.baseURL + 'register', model);
}
}
