using LearnMinimalApiEFCore.MovieAPI.Models;

namespace LearnMinimalApiEFCore.MovieAPI.Repositories
{
    public class MovieRepository
    {
        private readonly List<Movie> _movies = new();

        public List<Movie> GetAll() => _movies;

        public Movie? GetById(int id) => _movies.FirstOrDefault(m => m.Id == id);

        public void Add(Movie movie)
        {
            // Check if the movie with the same title and release year already exists
            if (_movies.Any(m => m.Title.Equals(movie.Title, StringComparison.OrdinalIgnoreCase) && m.ReleaseYear == movie.ReleaseYear))
            {
                throw new InvalidOperationException("A movie with the same title and release year already exists.");
            }

            movie.Id = _movies.Any() ? _movies.Max(m => m.Id) + 1 : 1;
            _movies.Add(movie);
        }

        public void Update(int id, Movie updatedMovie)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                movie.Title = updatedMovie.Title;
                movie.Director = updatedMovie.Director;
                movie.ReleaseYear = updatedMovie.ReleaseYear;
            }
        }
        public void Delete(int id) => _movies.RemoveAll(m => m.Id == id);
    }
}
