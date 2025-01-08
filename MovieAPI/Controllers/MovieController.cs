using LearnMinimalApiEFCore.MovieAPI.Models;
using LearnMinimalApiEFCore.MovieAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LearnMinimalApiEFCore.MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _repository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            return movie;
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (await _repository.ExistsAsync(movie.Title, movie.ReleaseYear))
            {
                return BadRequest("Movie already exists.");
            }

            var addedMovie = await _repository.AddAsync(movie);
            return CreatedAtRoute("GetMovie", new { id = addedMovie.Id }, addedMovie);
        }

        // PUT: api/Movies/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutMovie(int id, Movie updatedMovie)
        {
            if (id != updatedMovie.Id)
            {
                return BadRequest("ID mismatch in request body and URL.");
            }

            var movie = await _repository.UpdateAsync(id, updatedMovie);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            return Ok(movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (await _repository.DeleteAsync(id))
            {
                return Ok("Movie deleted successfully.");
            }

            return NotFound("Movie not found.");
        }
    }
}
