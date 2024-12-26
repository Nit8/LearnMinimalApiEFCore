using LearnMinimalApiEFCore.MovieAPI.EndPoints;
using LearnMinimalApiEFCore.MovieAPI.Repositories;
using LearnMinimalApiEFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace LearnMinimalApiEFCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Without Swagger
            //var builder = WebApplication.CreateBuilder(args);
            //var app = builder.Build();

            ////app.MapGet("/", () => "Hello World!");

            //// Dependency Injection
            //var repository = new MovieRepository();

            //// Map Endpoints
            //app.MapMovieEndpoints(repository);

            //app.Run();

            #endregion

            #region With Swagger

            //var builder = WebApplication.CreateBuilder(args);

            //// Add Swagger services
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie API", Version = "v1" });
            //});

            //var app = builder.Build();
            //app.MapGet("/", () => "Hello World!");

            //// Enable Swagger middleware
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c =>
            //    {
            //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API v1");
            //    });
            //}

            //// Map your endpoints here...
            //var repository = new MovieRepository();
            //app.MapMovieEndpoints(repository);

            //app.Run();

            #endregion

            // Create the WebApplication builder
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            // Add DbContext with MSSQL connection string
            builder.Services.AddDbContext<MovieDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IMovieRepository, MovieDapperRepository>();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Build the application
            var app = builder.Build();

            // Enable Swagger for Development Environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Middleware pipeline
            app.UseHttpsRedirection();

            // Map endpoints
            app.MapGet("/", () => "Welcome to the Movie API!"); // Test endpoint

            //var repository = new MovieRepository();
            app.MapMovieEndpoints();

            // Run the application
            app.Run();
        }
    }
}
