using Microsoft.AspNetCore.Mvc;

namespace WebBlog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
