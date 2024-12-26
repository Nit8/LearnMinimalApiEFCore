using Dapper;
using LearnMinimalApiEFCore.MovieAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LearnMinimalApiEFCore.MovieAPI.Repositories
{
    public class MovieDapperRepository : IMovieRepository
    {
        private readonly string _connectionString;

        public MovieDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            const string query = "SELECT * FROM Movies";
            using var connection = CreateConnection();
            return await connection.QueryAsync<Movie>(query);
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            const string query = "SELECT * FROM Movies WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Movie>(query, new { Id = id });
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            const string query = @"
                INSERT INTO Movies (Title, ReleaseYear)
                VALUES (@Title, @ReleaseYear);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var connection = CreateConnection();
            var id = await connection.ExecuteScalarAsync<int>(query, movie);
            movie.Id = id;
            return movie;
        }

        public async Task<Movie?> UpdateAsync(int id, Movie updatedMovie)
        {
            const string query = @"
                UPDATE Movies
                SET Title = @Title, ReleaseYear = @ReleaseYear
                WHERE Id = @Id";

            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                Id = id,
                Title = updatedMovie.Title,
                ReleaseYear = updatedMovie.ReleaseYear
            });

            return rowsAffected > 0 ? updatedMovie : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string query = "DELETE FROM Movies WHERE Id = @Id";
            using var connection = CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> ExistsAsync(string title, int releaseYear)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM Movies
                WHERE Title = @Title AND ReleaseYear = @ReleaseYear";

            using var connection = CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Title = title, ReleaseYear = releaseYear });
            return count > 0;
        }
    }
}
