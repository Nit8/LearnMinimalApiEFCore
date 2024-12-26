using LearnMinimalApiEFCore.Data;
using LearnMinimalApiEFCore.MovieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnMinimalApiEFCore.MovieAPI.Repositories
{
    public class MovieDBRepository : IMovieRepository
    {
        private readonly MovieDbContext _dbContext;

        public MovieDBRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _dbContext.Movies.FindAsync(id);
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> UpdateAsync(int id, Movie updatedMovie)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null) return null;

            movie.Title = updatedMovie.Title;
            movie.ReleaseYear = updatedMovie.ReleaseYear;

            await _dbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null) return false;

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string title, int releaseYear)
        {
            return await _dbContext.Movies.AnyAsync(m => m.Title == title && m.ReleaseYear == releaseYear);
        }
    }
}
