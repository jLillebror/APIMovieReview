using Microsoft.AspNetCore.Identity;

namespace APIMovieReview.Models
{
    public class CustomUser : IdentityUser
    {
        public List<MovieReview> Reviews { get; set; }
    }
}
