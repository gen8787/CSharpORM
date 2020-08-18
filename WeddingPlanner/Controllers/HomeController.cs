using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
// v~~ S E T U P   V I E W ~~v //
// Context (delete ILogger)
        private WeddingPlannerContext db;
        public HomeController(WeddingPlannerContext context)
        {
            db = context;
        }
// v~~ L O G I N   &   R E G   V I E W S ~~v //
// Register Page - Login Form
// Register - Create User in Db
// Login Page - Login Form
// Login - Validate LoginUser

// v~~ M A I N   V I E W S ~~v //
// Index Page - 
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }
//<~~ E N D   O F   M A I N   V I E W S ~~> //
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
