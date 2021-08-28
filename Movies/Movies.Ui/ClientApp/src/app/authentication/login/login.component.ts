import { Component, OnInit } from '@angular/core';
import { LoginDto } from '../../interfaces/login-dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    })
    this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }

  public loginUser = (loginFormValue) => {
    this.showError = false;
    const login = { ...loginFormValue };
    const userForAuth: LoginDto = {
      email: login.username,
      password: login.password
    }
    this._authService.loginUser(userForAuth)
      .subscribe(res => {
        localStorage.setItem("token", res.token);
        this._router.navigate([this._returnUrl]);
      },
        (error) => {
          this.errorMessage = error;
          this.showError = true;
        })
  }
}
