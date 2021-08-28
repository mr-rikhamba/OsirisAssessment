import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginDto } from '../interfaces/login-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http: HttpClient) { }

  Login(loginModel: LoginDto) {
    return this.http.post<string>(`${this._baseURL}api/SimpleAuth/Login`, loginModel);
  }
}
