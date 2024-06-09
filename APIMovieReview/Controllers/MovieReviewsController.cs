using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIMovieReview.Data;
using APIMovieReview.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIMovieReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieReviewsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MovieReviewsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/MovieReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReview>>> GetMovieReview()
        {
            return await _context.MovieReview.ToListAsync();
        }

        // GET: api/MovieReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReview>> GetMovieReview(int id)
        {
            var movieReview = await _context.MovieReview.FindAsync(id);

            if (movieReview == null)
            {
                return NotFound();
            }

            return movieReview;
        }

        // GET: api/MovieReviews/Movie/5
        [HttpGet("Movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<MovieReview>>> GetReviewsForMovie(int movieId)
        {
            var reviews = await _context.MovieReview
                                        .Where(r => r.MovieId == movieId)
                                        .ToListAsync();

            if (reviews == null || reviews.Count == 0)
            {
                return NotFound();
            }

            return reviews;
        }

        // GET: api/MovieReviews/User
        [HttpGet("User"), Authorize]
        public async Task<ActionResult<IEnumerable<MovieReview>>> GetReviewsByUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reviews = await _context.MovieReview
                                        .Where(r => r.UserId == userId)
                                        .ToListAsync();

            if (reviews == null || reviews.Count == 0)
            {
                return NotFound();
            }

            return reviews;
        }

        // PUT: api/MovieReviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutMovieReview(int id, MovieReview movieReview)
        {
            if (id != movieReview.Id)
            {
                return BadRequest();
            }

            _context.Entry(movieReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MovieReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<MovieReview>> PostMovieReview(MovieReview movieReview)
        {
            _context.MovieReview.Add(movieReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieReview", new { id = movieReview.Id }, movieReview);
        }

        // DELETE: api/MovieReviews/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteMovieReview(int id)
        {
            var movieReview = await _context.MovieReview.FindAsync(id);
            if (movieReview == null)
            {
                return NotFound();
            }

            _context.MovieReview.Remove(movieReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieReviewExists(int id)
        {
            return _context.MovieReview.Any(e => e.Id == id);
        }
    }
}
