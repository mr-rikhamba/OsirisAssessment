import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MovieModel } from '../Interfaces/movie-model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieServiceService {
  public headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${localStorage.getItem("token")}`
  })
  constructor(private http: HttpClient) { }

  
  getMovies() {
  
    return this.http.get<MovieModel[]>(`${environment.baseURL}/Api/Movies/Picks`, { headers: this.headers });
  }
  search(searchString:string) {
    return this.http.get<MovieModel[]>(`${environment.baseURL}/Api/Movies/Search/${searchString}`);
  }
}
