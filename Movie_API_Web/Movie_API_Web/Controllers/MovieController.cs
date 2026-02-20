using Microsoft.AspNetCore.Mvc;

namespace Movie_API_Web.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
