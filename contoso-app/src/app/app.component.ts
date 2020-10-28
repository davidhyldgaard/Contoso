import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMovie } from './models/movie';
import { MoviesService } from './movies.service';
import { IGenre } from './models/genre';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Movies App';
  movies: IMovie[];
  genres: IGenre[];

  constructor(private moviesService: MoviesService) { }

  ngOnInit() {
    this.getGenres();
  }

  getGenres() {
    this.moviesService.getGenres().subscribe((genres: IGenre[]) => {
      this.genres = genres;
      console.log(this.genres);
    });
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

  updateMovie(movie, updateArea: string, newValue: any) {
    this.moviesService.updateMovie(movie, updateArea, newValue)
      .subscribe(
        (success: IMovie) => console.log("PUT call successful", success),
        error => console.log("PUT call in error", error)
      );
  }

  newMovie(newName: string, newDesc: string, newDir: string, newCast: string, newDate: Date, newGenre: string) {
    var m: IMovie = {
      name: newName,
      description: newDesc,
      director: newDir,
      genre: newGenre,
      cast: newCast,
      releaseDate: newDate
    }

    this.moviesService.newMovie(m)
      .subscribe(
        (success: IMovie) => {
          console.log("POST call successful", success);
        },
        error => console.log("POST call in error", error)
      );
  }

  deleteMovie(movieName: string) {
    this.moviesService.deleteMovie(movieName).subscribe(
      (success: IMovie) => {
        console.log("POST call successful", success)
        this.movies = this.movies.filter(x => x.name.toLowerCase() !== movieName.toLowerCase())
      },
      error => console.log("POST call in error", error)
    );
  }

  findMoviesByGenre(genreInput: string) {
    this.moviesService.findMoviesByGenre(genreInput).subscribe((movies: IMovie[]) => {
      this.movies = movies;
      console.log(this.movies);
    }, error => console.log(error));
  }

}
