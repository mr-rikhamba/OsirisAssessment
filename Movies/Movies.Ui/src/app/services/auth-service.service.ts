import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from '../Interfaces/login';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http: HttpClient) { }

  Login(login: Login) {
    var result = this.http.post<any>(`${environment.baseURL}/Api/SimpleAuth/Login`, login);
    console.log(result);
    return result;
  }
}
