import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IMovie } from './models/movie';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMovies = () => this.http.get(this.baseUrl + 'movies');
  getMovie = (att: string, value: string) => this.http.get(this.baseUrl + `movies/${att}/${value}`);
  deleteMovie = (movieName: string) => this.http.delete(this.baseUrl + `movies/${movieName}`);
  getGenres = () => this.http.get(this.baseUrl + 'genre');
  findMoviesByGenre = (genreInput: string) => this.http.get(this.baseUrl + `genre/${genreInput}`);

  updateMovie(movie: IMovie, area: string, val: any) {
    const headers = new HttpHeaders().set("Content-Type", "application/json");
    var m = movie;
    switch (area.toLowerCase()) {
      case "director":
        m.director = val;
        break;
      case "description":
        m.description = val;
        break;
      case "releasedate":
        m.releaseDate = new Date(val);
        break;
      case "genre":
        m.genre = val;
        break;
      case "cast":
        m.cast = val;
        break;
    }
    var body = JSON.stringify(m);
    return this.http.put(this.baseUrl + "movies", body, { headers });
  }

  newMovie(movie: IMovie) {
    const headers = new HttpHeaders().set("Content-Type", "application/json");
    var body = JSON.stringify(movie);
    return this.http.post(this.baseUrl + "movies", body, { headers });
  }
}
