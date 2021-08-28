import { Component, OnInit } from '@angular/core';
import { MovieModel } from '../Interfaces/movie-model';
import { MovieServiceService } from '../services/movie-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public movies: MovieModel[];
  constructor(private _movieService: MovieServiceService) { 
    this.movies = [];
  }

  ngOnInit(): void {
    this._movieService.getMovies().subscribe(data => {
        this.movies = data;
    })
  }

}
