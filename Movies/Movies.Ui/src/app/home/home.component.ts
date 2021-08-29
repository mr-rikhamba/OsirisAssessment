import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MovieModel } from '../Interfaces/movie-model';
import { MovieServiceService } from '../services/movie-service.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public searchForm: any;
  public movies: MovieModel[];
  public isIsLoading: boolean = false;

  constructor(private movieService: MovieServiceService, private router: Router, public dialog: MatDialog) {
    this.movies = [];
  }

  ngOnInit(): void {
    this.searchForm = new FormGroup({
      searchString: new FormControl("", [Validators.required])
    });
    this.isIsLoading = true;
    this.movieService.getMovies().subscribe(data => {
      this.isIsLoading = false;
      this.processResult(data);
    }, (error: HttpErrorResponse) => {
      this.isIsLoading = false;
      switch (error.status) {
        case 401:
          this.openDialog("Your session has expired, please login again.");
          this.router.navigateByUrl("");
          break;
        default:
          this.openDialog(error.message);
          break;
      }
    });
  }
  openDialog(message: string) {
    const dialogRef = this.dialog.open(DialogComponent, {
      disableClose: true,
      data: message
    });
  }
  public Search(searchForm: any) {
    if  (searchForm.searchString === ''){
      this.openDialog("Please enter a valid movie name.");
      return;
    }
    this.isIsLoading = true;
    this.movieService.search(searchForm.searchString).subscribe(data => {
      this.isIsLoading = false;
     this.processResult(data);
    }, (error: HttpErrorResponse) => {
      this.isIsLoading = false;
      switch (error.status) {
        case 401:
          this.openDialog("Your session has expired, please login again.");
          this.router.navigateByUrl("");
          break;
        default:
          this.openDialog(error.message);
          break;
      }
    });
  }
  private processResult(data: MovieModel[]): void{
   if (data.length > 0){
    this.movies = data;
   }else  {
     this.openDialog("No records returned.");
   }
  }
}
@Component({
  selector: 'dialog-data-example-dialog',
  templateUrl: 'dialog.component.html'
})
export class DialogComponent {
    constructor(public dialogRef: MatDialogRef<DialogComponent>, @Inject(MAT_DIALOG_DATA) public message: string, public router: Router) { }
  closeThis(): void {
    this.dialogRef.close();
  }

}