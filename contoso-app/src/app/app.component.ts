import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMovie } from './models/movie';
import { MoviesService } from './movies.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Movies App';
  movies: IMovie[];

  constructor(private moviesService: MoviesService) { }

  ngOnInit() {
  }

  getMovies() {
    this.moviesService.getMovies().subscribe((movies: IMovie[]) => {
      this.movies = movies;
      console.log(this.movies);
    }, error => console.log(error));
  }

  findMovie(att, val) {  
    this.moviesService.getMovie(`${att}`, `${val}`).subscribe((movie: IMovie[]) => {
      this.movies = movie;
      console.log(this.movies);
    }, error => console.log(error));
  }

  findMovie2() {  
    this.moviesService.getMovie("name","the two towers").subscribe((movie: IMovie[]) => {
      this.movies = movie;
      console.log(this.movies);
    }, error => console.log(error));
  }
}
