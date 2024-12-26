using LearnMinimalApiEFCore.MovieAPI.Models;

namespace LearnMinimalApiEFCore.MovieAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task<Movie> AddAsync(Movie movie);
        Task<Movie?> UpdateAsync(int id, Movie updatedMovie);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(string title, int releaseYear);
    }
}
