using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
namespace WebBlog.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            
            return View();
        }
    }
}
