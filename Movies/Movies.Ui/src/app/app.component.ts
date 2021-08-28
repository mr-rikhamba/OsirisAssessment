import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DarkModeService } from 'angular-dark-mode';
import { AuthServiceService } from './services/auth-service.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'MoviesUi';
  public isUserAuthenticated: boolean = false;

  constructor(private darkModeService: DarkModeService, private authServiceService: AuthServiceService, private router: Router) {
    this.isUserAuthenticated = localStorage.getItem("token") != null;

  }

  ngOnInit(): void {
    this.authServiceService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  }

  public logout() {

    localStorage.clear();
    this.isUserAuthenticated = false;
    this.router.navigateByUrl("");
  }
  public toggleDark(){
    this.darkModeService.toggle();
  }
}
