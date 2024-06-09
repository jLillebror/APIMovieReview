using System.ComponentModel.DataAnnotations;

namespace APIMovieReview.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }

        // Navigation property for reviews
        public ICollection<MovieReview> Reviews { get; set; }
    }
}
