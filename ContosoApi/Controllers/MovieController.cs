using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contoso.Controllers
{
    [ApiController]
    [Route("/movies")]
    public class MovieController : ControllerBase
    {

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<Movie> GetAll()
        {
            return MovieDbHandler.GetAllMovies().ToArray();
        }

        [HttpGet("{att}/{value}")]
        public IEnumerable<Movie> GetMovie(string att, string value)
        {
            switch (att.ToLower())
            {
                case "name":
                    {
                        return new List<Movie> { MovieDbHandler.GetMovieByName(value) };
                    }
                case "director":
                    {
                        return MovieDbHandler.GetMoviesByDirector(value);
                    }
                case "release date":
                    {
                        return MovieDbHandler.GetMoviesByReleaseDate(value);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        [HttpPut("name/{oldval}/{newval}")]
        public Movie UpdateName(string oldval, string newval)
        {
            return MovieDbHandler.UpdateName(oldval, newval);
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        public Movie NewMovie(Movie movie)
        {
            return MovieDbHandler.NewMovie(movie);
        }


        [HttpDelete("{name}")]
        public bool DeleteMovie(string name)
        {
            return MovieDbHandler.DeleteMovie(name);
        }
    }

    [ApiController]
    [Route("/genres")]
    public class GenreController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<String> GetAllGenres()
        {
            return MovieDbHandler.GetAllMovies().Select(x => x.Genre).Distinct();
        }

        [HttpGet("{genre}")]
        public IEnumerable<Movie> GetMoviesByGenre(string genre)
        {
            return MovieDbHandler.GetAllMovies().ToList().FindAll(x => x.Genre.ToLower().Equals(genre.ToLower()));
        }
    }
}
