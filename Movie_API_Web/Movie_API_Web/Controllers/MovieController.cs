using Microsoft.AspNetCore.Mvc;
using Movie_API_Web.Models;
using Movie_API_Web.Services;

namespace Movie_API_Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly TmdbService _tmdbService;
        private readonly CommentService _commentService;

        public MovieController(TmdbService tmdbService, CommentService commentService)
        {
            _tmdbService = tmdbService;
            _commentService = commentService;
        }

        /// <summary>
        /// Displays list of movies with filtering and search capabilities
        /// </summary>
        /// <param name="searchQuery">Optional search term to filter movies</param>
        /// <param name="category">Filter by category: popular, upcoming, nowplaying</param>
        /// <param name="page">Page number for pagination (default 1)</param>
        /// <returns>View with filtered movie list</returns>
        public async Task<IActionResult> Index(string searchQuery = "", string category = "popular", int page = 1)
        {
            try
            {
                SearchResult result = new SearchResult();

                // Determine which API endpoint to call based on category
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    // If user entered a search query, search for movies
                    result = await _tmdbService.SearchMoviesAsync(searchQuery, page);
                    ViewBag.SearchQuery = searchQuery;
                }
                else
                {
                    // Otherwise show movies by category
                    result = category switch
                    {
                        "upcoming" => await _tmdbService.GetUpcomingMoviesAsync(page),
                        "nowplaying" => await _tmdbService.GetNowPlayingMoviesAsync(page),
                        _ => await _tmdbService.GetPopularMoviesAsync(page)
                    };
                }

                ViewBag.Category = category;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = result.TotalPages;
                ViewBag.ImageBaseUrl = TmdbService.IMAGE_BASE_URL;

                return View(result.Results);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading movies: {ex.Message}";
                return View(new List<Movie>());
            }
        }

        /// <summary>
        /// Displays detailed view of a single movie with comments
        /// </summary>
        /// <param name="id">The movie ID to display</param>
        /// <returns>View with movie details and comments</returns>
        public IActionResult Details(int id)
        {
            try
            {
                // In a production app, you would fetch the movie from TMDb here
                // For now, we'll store the movie ID and fetch comments

                // Get comments for this movie
                List<Comment> comments = _commentService.GetCommentsByMovieId(id);

                ViewBag.MovieId = id;
                ViewBag.ImageBaseUrl = TmdbService.IMAGE_BASE_URL;
                ViewBag.Comments = comments;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading movie details: {ex.Message}";
                return View();
            }
        }

        /// <summary>
        /// Handles adding a new comment to a movie
        /// Accepts POST request with comment data
        /// </summary>
        /// <param name="movieId">The movie ID to comment on</param>
        /// <param name="author">Name of the commenter</param>
        /// <param name="email">Email of the commenter</param>
        /// <param name="message">The comment message</param>
        /// <returns>Redirect back to movie details</returns>
        [HttpPost]
        public IActionResult AddComment(int movieId, string author, string email, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    ViewBag.Error = "Comment cannot be empty";
                    return RedirectToAction("Details", new { id = movieId });
                }

                Comment comment = new Comment
                {
                    MovieId = movieId,
                    Author = author ?? "Anonymous",
                    Email = email ?? "noemail@example.com",
                    Message = message
                };

                _commentService.AddComment(comment);

                return RedirectToAction("Details", new { id = movieId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error adding comment: {ex.Message}";
                return RedirectToAction("Details", new { id = movieId });
            }
        }

        /// <summary>
        /// Handles adding a reply to an existing comment
        /// </summary>
        /// <param name="movieId">The movie ID</param>
        /// <param name="commentId">The comment ID to reply to</param>
        /// <param name="author">Name of the replier</param>
        /// <param name="email">Email of the replier</param>
        /// <param name="message">The reply message</param>
        /// <returns>Redirect back to movie details</returns>
        [HttpPost]
        public IActionResult AddReply(int movieId, string commentId, string author, string email, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    return RedirectToAction("Details", new { id = movieId });
                }

                Reply reply = new Reply
                {
                    Author = author ?? "Anonymous",
                    Email = email ?? "noemail@example.com",
                    Message = message
                };

                _commentService.AddReply(movieId, commentId, reply);

                return RedirectToAction("Details", new { id = movieId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error adding reply: {ex.Message}";
                return RedirectToAction("Details", new { id = movieId });
            }
        }
    }
}