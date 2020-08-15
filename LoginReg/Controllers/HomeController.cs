using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoginReg.Models;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
// v~~ M A I N   V I E W S ~~v //
// Context (delete ILogger)
        private LoginRegContext db;
        public HomeController(LoginRegContext context)
        {
            db = context;
        }
// Index
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }
// Register
        [HttpPost("register")]
        public IActionResult Register()
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
