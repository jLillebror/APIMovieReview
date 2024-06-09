using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace APIMovieReview.Models
{
    public class MovieReview
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        [ValidateNever, ForeignKey("CustomUser")]
        public string UserId { get; set; }
        [ValidateNever]
        public CustomUser CustomUser { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        // Navigation property for reviews
        [ValidateNever]
        public Movie Movie { get; set; }
    }
}
