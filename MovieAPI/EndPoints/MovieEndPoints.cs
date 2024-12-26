using LearnMinimalApiEFCore.MovieAPI.Models;
using LearnMinimalApiEFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace LearnMinimalApiEFCore.MovieAPI.EndPoints
{
    public static class MovieEndPoints
    {
        public static void MapMovieEndpoints(this WebApplication app)
        {
            // GET: Retrieve all movies
            app.MapGet("/movies", async (MovieDbContext db) =>
            {
                return await db.Movies.ToListAsync();
            });

            // GET: Retrieve a single movie by ID
            app.MapGet("/movies/{id:int}", async (MovieDbContext db, int id) =>
            {
                var movie = await db.Movies.FindAsync(id);
                return movie is not null ? Results.Ok(movie) : Results.NotFound("Movie not found.");
            });

            // POST: Add a new movie
            app.MapPost("/movies", async (MovieDbContext db, Movie movie) =>
            {
                // Check if the movie already exists
                if (await db.Movies.AnyAsync(m => m.Title == movie.Title && m.ReleaseYear == movie.ReleaseYear))
                    return Results.BadRequest("Movie already exists.");

                // Add the movie to the database
                db.Movies.Add(movie);
                await db.SaveChangesAsync();

                return Results.Created($"/movies/{movie.Id}", movie);
            });

            // PUT: Update an existing movie
            app.MapPut("/movies/{id:int}", async (MovieDbContext db, int id, Movie updatedMovie) =>
            {
                var movie = await db.Movies.FindAsync(id);
                if (movie is null)
                    return Results.NotFound("Movie not found.");

                // Update the movie details
                movie.Title = updatedMovie.Title;
                movie.ReleaseYear = updatedMovie.ReleaseYear;

                await db.SaveChangesAsync();
                return Results.Ok(movie);
            });

            // DELETE: Remove a movie
            app.MapDelete("/movies/{id:int}", async (MovieDbContext db, int id) =>
            {
                var movie = await db.Movies.FindAsync(id);
                if (movie is null)
                    return Results.NotFound("Movie not found.");

                // Remove the movie from the database
                db.Movies.Remove(movie);
                await db.SaveChangesAsync();

                return Results.Ok("Movie deleted successfully.");
            });
        }
    }
}
