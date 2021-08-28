import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MovieModel } from '../Interfaces/movie-model';
import { MovieServiceService } from '../services/movie-service.service';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public movies: MovieModel[];
  constructor(private movieService: MovieServiceService, private router: Router, public dialog: MatDialog) {
    this.movies = [];
  }

  ngOnInit(): void {
    this.movieService.getMovies().subscribe(data => {
      this.movies = data;
    }, (error: HttpErrorResponse) => {
      switch (error.status) {
        case 401:
          this.openDialog("Your session has expired, please login again.");
          this.router.navigateByUrl("");
          break;
        default:
          this.openDialog(error.message);
          break;
      }
    })
  }
  openDialog(message:string) {
    this.dialog.open(DialogComponent, {
      disableClose: true,
      data: message
    });
  }
  public openLogin(){
    this.router.navigateByUrl("");
      }
}
@Component({
  selector: 'dialog-data-example-dialog',
  template: '<h1 mat-dialog-title>An error occurred</h1><div mat-dialog-content>{{message}}<div mat-dialog-actions align="end"> <button mat-button mat-dialog-close>Cancel</button></div></div>'
})
export class DialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public message: string, public router: Router) {}
  public openLogin(){
this.router.navigateByUrl("");
  }
}