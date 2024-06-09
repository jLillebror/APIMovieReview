using APIMovieReview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIMovieReview.Data
{
    public class DatabaseContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<CustomUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieReview> MovieReview { get; set; }
    }

}
