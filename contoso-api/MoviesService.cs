using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ContosoApi
{

    public class MoviesService
    {
        private readonly ILogger _logger;
        public MoviesService(ILogger<MoviesService> logger)
        => _logger = logger;
        private List<Movie> _movies;
        private List<Movie> _deleted_movies;

        public IEnumerable<Movie> GetAllMovies()
        {
            if (_movies == null) populate_mock_db();
            return _movies;
        }

        public Movie GetMovieByName(string name)
        {
            if (_movies == null) populate_mock_db();
            return _movies.Find(x => x.Name.ToUpper().Equals(name.ToUpper()));
        }

        public IEnumerable<Movie> GetMoviesByReleaseDate(string value)
        {
            if (_movies == null) populate_mock_db();
            return _movies.FindAll(m => m.ReleaseDate.CompareTo(Convert.ToDateTime(value)) == 0);
        }

        public IEnumerable<Movie> GetMoviesByDirector(string value)
        {
            if (_movies == null) populate_mock_db();
            return _movies.FindAll(m => m.Director.ToLower().Equals(value.ToLower()));
        }

        public Movie UpdateMovie(Movie movie)
        {
            if (_movies == null) populate_mock_db();

            int mi = _movies.FindIndex(x => x.Name.ToUpper().Equals(movie.Name.ToUpper()));
            if (mi != -1)
                _movies[mi] = movie;
            return _movies[mi];
        }

        public bool DeleteMovie(String name)
        {
            return _movies.RemoveAll(x => x.Name.ToLower().Equals(name.ToLower())) > 0;
        }

        public Movie NewMovie(Movie movie)
        {
            if (_movies == null) populate_mock_db();
            if (!canAddMovie(movie)) return null;

            _movies.Add(movie);
            return _movies.Find(x => x == movie);
        }

        private bool canAddMovie(Movie movie)
        {
            if (_deleted_movies.Exists(x => x.Name.ToLower().Equals(movie.Name.ToLower()))) return false;
            if (_movies.Exists(x => x.Name.ToLower().Equals(movie.Name.ToLower()))) return false;
            return true;
        }

        private void populate_mock_db()
        {
            _deleted_movies = new List<Movie>();
            _movies = new List<Movie>{
                new Movie() {
                    Name = "The Fellowship Of The Ring",
                    Description = "Frodo enters adventure",
                    ReleaseDate = new DateTime(2003,12,12),
                    Cast = "Viggo Mortensen, Orlando Bloom, Elisha Woods",
                    Director = "Peter Jackson",
                    Genre = "Adventure"
                },
                new Movie {
                    Name = "The Two Towers",
                    Description = "Frodo is on adventure",
                    ReleaseDate = new DateTime(2004,12,12),
                    Cast = "Viggo Mortensen, Orlando Bloom, Elisha Woods",
                    Director = "Peter Jackson",
                    Genre = "Adventure"
                },
                new Movie {
                    Name = "The King Returns",
                    Description = "Frodo completes adventure",
                    ReleaseDate = new DateTime(2005,12,12),
                    Cast = "Viggo Mortensen, Orlando Bloom, Elisha Woods",
                    Director = "Peter Jackson",
                    Genre = "Adventure"
                },
                new Movie {
                    Name = "Finding Nemo",
                    Description = "Nemo's adventure",
                    ReleaseDate = new DateTime(2003,12,12),
                    Cast = "Ellen DeGenerous",
                    Director = "Jason Bourne",
                    Genre = "Family"
                }
            };
        }
    }
}