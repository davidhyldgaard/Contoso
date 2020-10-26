using System;
using System.Collections.Generic;

namespace Contoso
{

    public class MovieDbHandler
    {
        private static List<Movie> _movies;
        private MovieDbHandler()
        {
            _deleted_movies = new List<Movie>();
            _movies = new List<Movie>();
        }

        private static List<Movie> _deleted_movies;

        internal static IEnumerable<Movie> GetAllMovies()
        {
            if (_movies == null) populate_mock_db();

            return _movies;
        }

        internal static Movie GetMovieByName(string name)
        {
            if (_movies == null) populate_mock_db();

            return _movies.Find(x => x.Name.ToUpper().Equals(name.ToUpper()));
        }

        internal static IEnumerable<Movie> GetMoviesByReleaseDate(string value)
        {
            if (_movies == null) populate_mock_db();

            return _movies.FindAll(m => m.ReleaseDate.CompareTo(Convert.ToDateTime(value)) == 0);
        }

        internal static IEnumerable<Movie> GetMoviesByDirector(string value)
        {
            if (_movies == null) populate_mock_db();

            return _movies.FindAll(m => m.Director.ToLower().Equals(value.ToLower()));
        }

        internal static Movie UpdateName(string name, string newName)
        {
            if (_movies == null) populate_mock_db();

            Movie m = _movies.Find(x => x.Name.ToUpper().Equals(name.ToUpper()));
            if (m != null)
                m.Name = newName;
            return m;
        }

        internal static bool DeleteMovie(string name)
        {
            Movie m = _movies.Find(m => m.Name.ToLower().Equals(name.ToLower()));
            if (m != null)
            {
                _movies.Remove(m);
                _deleted_movies.Add(m);
                return true;
            }

            return false;
        }

        internal static Movie NewMovie(Movie movie)
        {
            if (_movies == null) populate_mock_db();

            if (!canAddMovie(movie)) return null;

            _movies.Add(movie);
            return movie;
        }

        private static bool canAddMovie(Movie movie)
        {
            if (_deleted_movies.Exists(x => x.Name.ToLower().Equals(movie.Name.ToLower()))) return false;
            if (_movies.Exists(x => x.Name.ToLower().Equals(movie.Name.ToLower()))) return false;
            return true;
        }

        private static void populate_mock_db()
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