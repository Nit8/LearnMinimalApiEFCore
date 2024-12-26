using System.ComponentModel.DataAnnotations;

namespace LearnMinimalApiEFCore.MovieAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        [Range(1980, 2025)]
        public int ReleaseYear { get; set; }
    }
}
