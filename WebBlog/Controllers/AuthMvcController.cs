using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace WebBlog.Controllers
{
    public class AuthMvcController : Controller
    {
        private const string Path = "https://localhost:7146/api/";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAuthDto request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Path);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("Auth/Login", request);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    var HasUser = User.Identity.Name;
                    var active = User.Identity.IsAuthenticated;
                    ViewBag.HasUser = HasUser;
                    
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.Hata = "Server Hatası";
                    return View();
                }
            }
        }


    }
}
