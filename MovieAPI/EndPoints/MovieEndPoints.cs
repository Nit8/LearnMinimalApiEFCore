using LearnMinimalApiEFCore.MovieAPI.Models;
using LearnMinimalApiEFCore.MovieAPI.Repositories;

namespace LearnMinimalApiEFCore.MovieAPI.EndPoints
{
    public static class MovieEndPoints
    {
        public static void MapMovieEndpoints(this WebApplication app)
        {
            // GET: Retrieve all movies
            app.MapGet("/movies", async (IMovieRepository repository) =>
            {
                return Results.Ok(await repository.GetAllAsync());
            });

            // GET: Retrieve a single movie by ID
            app.MapGet("/movies/{id:int}", async (IMovieRepository repository, int id) =>
            {
                var movie = await repository.GetByIdAsync(id);
                return movie is not null ? Results.Ok(movie) : Results.NotFound("Movie not found.");
            });

            // POST: Add a new movie
            app.MapPost("/movies", async (IMovieRepository repository, Movie movie) =>
            {
                if (await repository.ExistsAsync(movie.Title, movie.ReleaseYear))
                    return Results.BadRequest("Movie already exists.");

                var addedMovie = await repository.AddAsync(movie);
                return Results.Created($"/movies/{addedMovie.Id}", addedMovie);
            });

            // PUT: Update an existing movie
            app.MapPut("/movies/{id:int}", async (IMovieRepository repository, int id, Movie updatedMovie) =>
            {
                var movie = await repository.UpdateAsync(id, updatedMovie);
                return movie is not null ? Results.Ok(movie) : Results.NotFound("Movie not found.");
            });

            // DELETE: Remove a movie
            app.MapDelete("/movies/{id:int}", async (IMovieRepository repository, int id) =>
            {
                return await repository.DeleteAsync(id)
                    ? Results.Ok("Movie deleted successfully.")
                    : Results.NotFound("Movie not found.");
            });
        }
    }
}
