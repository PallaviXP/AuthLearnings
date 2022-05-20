using BasicWebAppEp1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace BasicWebAppEp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Authenticate()
        {
            var claims1 = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "pal"),
                new Claim(ClaimTypes.Email, "pal@fmail.com") ,
                new Claim("customtext", "Hi Pal!") ,
            };
            var claims2 = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "pal 1"),
                 new Claim("customtext driver license", "pal 2"),
            };

            var palIdentity = new ClaimsIdentity(claims1, "pal identity");
            var driverlicenseIdentity = new ClaimsIdentity(claims2, "driver identity");

            //user has multiple identities
            var userpricipal = new ClaimsPrincipal(new[] { palIdentity , driverlicenseIdentity });

            //SignInAsync is an extension method that lives in
            ////Microsoft.AspNetCore.Authentication.Abstractions and 
            /////lives inside the Microsoft.AspNetCore.Authentication namespace.
            HttpContext.SignInAsync(userpricipal);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}