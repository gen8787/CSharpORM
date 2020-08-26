using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MapProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace MapProject.Controllers
{
    public class HomeController : Controller
    {
// v~~ S E T U P   V I E W S ~~v //
    // Context (delete ILogger)
        private MapProjectContext db;
        public HomeController(MapProjectContext context)
        {
            db = context;
        }
    // Convert current user in Session to uid
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("userId");
            }
        }
    // Returns true or false if current user is logged in
        private bool isLoggedIn
        {
            get
            {
                return uid != null;
            }
        }
// v~~ L O G I N   &   R E G   V I E W S ~~v //
    // Register Page -> Register Form
        // Displayed on Index Page

    // Register -> Create User in Db
        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered, please login.");
                    return View("Index");
                }
                Microsoft.AspNetCore.Identity.PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                db.Users.Add(newUser);
                db.SaveChanges();
                HttpContext.Session.SetInt32("userId", newUser.UserId);
                HttpContext.Session.SetString("userFirstName", newUser.FirstName);
                return RedirectToAction("Dashboard", newUser.UserId); 
            }
            return View("Index");
        }
    // Login Page -> Login Form
        // Displayed on Index Page

    // Login -> Validate LoginUser
        [HttpPost("process-login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if(ModelState.IsValid)
            {
                var userInDb = db.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid email address. Please try again or register.");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();            
                var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid passowrd. Please try again or register.");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("userId", userInDb.UserId);
                HttpContext.Session.SetString("userFirstName", userInDb.FirstName);
                return RedirectToAction("Dashboard", userInDb.UserId); 
            }
            return View("Index");
        }
// v~~ M A I N   V I E W S ~~v //
    // Index Page -> Login & Reg Forms
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }
    // Dashboard Page -> All Tours Summary
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return View("Dashboard");
        }
    // Logout -> Clear Session
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
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
