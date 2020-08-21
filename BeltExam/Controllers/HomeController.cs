using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
// v~~ S E T U P   V I E W S ~~v //
    // Context (delete ILogger)
        private BeltExamContext db;
        public HomeController(BeltExamContext context)
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
        // Displayed on SignIn Page

    // Register -> Create User in Db
        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered, please login.");
                    return View("SignIn");
                }
                Microsoft.AspNetCore.Identity.PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                db.Users.Add(newUser);
                db.SaveChanges();
                HttpContext.Session.SetInt32("userId", newUser.UserId);
                HttpContext.Session.SetString("userFirstName", newUser.FirstName);
                return RedirectToAction("Home", newUser.UserId); 
            }
            return View("SignIn");
        }
    // Login Page -> Login Form
        // Displayed on SignIn Page

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
                    return View("SignIn");
                }
                var hasher = new PasswordHasher<LoginUser>();            
                var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid passowrd. Please try again or register.");
                    return View("SignIn");
                }
                HttpContext.Session.SetInt32("userId", userInDb.UserId);
                HttpContext.Session.SetString("userFirstName", userInDb.FirstName);
                return RedirectToAction("Home", userInDb.UserId); 
            }
            return View("SignIn");
        }
// v~~ M A I N   V I E W S ~~v //
    // Index Page -> Login & Reg Forms
        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }
    // SignIn Page
        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return View("SignIn");
        }
    // Home -> All Activities
        [HttpGet("home")]
        public IActionResult Home()
        {
            List<AnActivity> AllActivities = db.AnActivities
                .Include(u => u.RelatedParticipants)
                .Include(u => u.AnActivityCreator)
                .OrderBy(d => d.Date)
                .ToList();

            return View("Home", AllActivities);
        }
    // New Activity Page -> New Activity Form
        [HttpGet("new")]
        public IActionResult NewActivity()
        {
            return View("NewActivity");
        }
    // Create Activity -> Add to Db
        [HttpPost("create-activity")]
        public IActionResult CreateActivity(AnActivity newAnActivity)
        {
            if (ModelState.IsValid)
            {
                if (newAnActivity.Date <= DateTime.Now)
                {
                    ModelState.AddModelError("Date", "Activity must be a future date.");
                    return View("NewActivity", newAnActivity);
                }
                newAnActivity.UserId = (int)uid;
                db.AnActivities.Add(newAnActivity);
                db.SaveChanges();
                return RedirectToAction("Home"); 
            }
            return View("NewActivity", newAnActivity);
        }
    // View Activity Details Page
        [HttpGet("activity/{AnActivityId}")]
        public IActionResult AnActivity(int AnActivityId)
        {
            AnActivity thisAnActivity = db.AnActivities
                .Include(x => x.AnActivityCreator)
                .Include(p => p.RelatedParticipants)
                .ThenInclude(rel => rel.Users)
                .FirstOrDefault(a => a.AnActivityId == AnActivityId);

            // List<User> usersParticipating = db.Users
            //     .Where(p => p.RelatedAnActivities.Any(a => a.AnActivityId == AnActivityId))
            //     .ToList();
            // ViewBag.usersParticipating = usersParticipating;

            return View("AnActivity", thisAnActivity);
        }
    // Join Activity -> Add Relationship
        [HttpGet("join/{AnActivityId}")]
        public IActionResult Join(Relationship newRelationship, int AnActivityId)
        {
            newRelationship.AnActivityId = AnActivityId;
            newRelationship.UserId = (int)uid;
            db.Relationships.Add(newRelationship);
            db.SaveChanges();
            return RedirectToAction("Home");
        }
    // Leave Activity -> Remove Relationship
        [HttpGet("leave/{AnActivityId}")]
        public IActionResult Leave(int AnActivityId)
        {
            Relationship RelationshipToDelete = db.Relationships.
            FirstOrDefault(r => r.UserId == uid && r.AnActivityId == AnActivityId);
            db.Relationships.Remove(RelationshipToDelete);
            db.SaveChanges();
            return RedirectToAction("Home");
        }
    // Delete Activity -> Remove from Db
        [HttpGet("delete/{AnActivityId}")]
        public IActionResult Delete(int AnActivityId)
        {
            AnActivity ActivityToDelete = db.AnActivities.FirstOrDefault(a => a.AnActivityId == AnActivityId);
            db.AnActivities.Remove(ActivityToDelete);
            db.SaveChanges();
            return RedirectToAction("Home");
        }
    // Logout -> Clear Session
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
