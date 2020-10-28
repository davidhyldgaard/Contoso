using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContosoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {

        private readonly MoviesService _moviesService;

        public MoviesController(MoviesService moviesService)
            => _moviesService = moviesService;

        [HttpGet]
        public IActionResult GetMovies()
            => Ok(_moviesService.GetAllMovies());


        [HttpGet("{att}/{value}")]
        public IActionResult GetMovie(string att, string value)
        {
            switch (att.ToLower())
            {
                case "name":
                    {
                        return Ok(new List<Movie> { _moviesService.GetMovieByName(value) });
                    }
                case "director":
                    {
                        return Ok(_moviesService.GetMoviesByDirector(value));
                    }
                case "release date":
                    {
                        return Ok(_moviesService.GetMoviesByReleaseDate(value));
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult UpdateMovie(Movie movie)
            => Ok(_moviesService.UpdateMovie(movie));


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult NewMovie(Movie movie)
            => Ok(_moviesService.NewMovie(movie));


        [HttpDelete("{name}")]
        public IActionResult DeleteMovie(String name)
            => Ok(_moviesService.DeleteMovie(name));

    }

    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly MoviesService _movieDbHandler;

        public GenreController(MoviesService moviesService)
            => _movieDbHandler = moviesService;

        [HttpGet]
        public IActionResult GetAllGenres()
            => Ok(_movieDbHandler.GetAllMovies().Select(x => x.Genre).Distinct());


        [HttpGet("{genre}")]
        public IActionResult GetMoviesByGenre(string genre)
            => Ok(_movieDbHandler.GetAllMovies().ToList().FindAll(x => x.Genre.ToLower().Equals(genre.ToLower())));

    }
}
