using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrewBlissApp.Controllers
{
    public class AdminController : Controller
    {
        
        public IActionResult Index(string returnUrl = "/Admin/Dashboard")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = "/Admin/Dashboard")
        {
            if (username == "admin" && password == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Redirect("Dashboard");
            }

            ViewBag.Error = "Invalid credentials.";
            ViewBag.ReturnUrl = returnUrl;
            return View("Index");
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}