import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Login } from 'src/app/Interfaces/login';
import { AuthServiceService } from 'src/app/services/auth-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: any;
  public errorMessage: string = '';
  private _returnUrl: string = "home";


  constructor(private _authService: AuthServiceService, private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    })

  }
  public loginUser = (loginFormValue: Login) => {

    this._authService.Login(loginFormValue)
      .subscribe(res => {
        localStorage.setItem("token", res.token);

        this._authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
        this._router.navigateByUrl(this._returnUrl);
      }, (error) => {
        console.log(error);
      });
  }
}
