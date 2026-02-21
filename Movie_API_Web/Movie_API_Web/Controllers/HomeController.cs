using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movie_API_Web.Models;
using Movie_API_Web.Services;

namespace Movie_API_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly TmdbService _tmdbService;

        public HomeController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        
        public async Task <IActionResult> Index()
        {
            try
            {
                var popularMovies = await _tmdbService.GetPopularMoviesAsync();
                ViewBag.FeatureMovies = popularMovies.Results.Take(6).ToList();

                var upcomingMovies = await _tmdbService.GetUpcomingMoviesAsync();
                ViewBag.UpcomingMovies = upcomingMovies.Results.Take(6).ToList();

                return View();

            }
            catch (Exception ex) {
                ViewBag.Error = $"Error loading movies: {ex.Message}";
                return View();
            }
            
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
