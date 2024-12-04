using Microsoft.AspNetCore.Mvc;

namespace Project4.Controllers
{
    public class ShowingController : Controller
    {
        public IActionResult Showing()
        {
            return View();
        }
    }
}
