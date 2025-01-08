using LearnMinimalApiEFCore.MovieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnMinimalApiEFCore.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd(); // Auto-increment Id
        }
    }
}
