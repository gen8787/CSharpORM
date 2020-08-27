using System.Linq;
using MapProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MapProject.Controllers
{
    public class TourController : Controller
    {
// v~~ S E T U P   V I E W S ~~v //
    // Context (delete ILogger)
        private MapProjectContext db;
        public TourController(MapProjectContext context)
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
// v~~ M A I N   V I E W S ~~v //
    // Create Tour -> Add to Db
        [HttpPost("add-tour")]
        public IActionResult CreateTour(Tour newTour)
        {
            if (ModelState.IsValid)
            {
                newTour.UserId = (int)uid;

                db.Tours.Add(newTour);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home"); 
            }
            return View("Dashboard", "Home");
        }
    // Delete Tour
        [HttpGet("delete/{TourId}")]
        public IActionResult Delete(int TourId)
        {
            Tour TourToDelete = db.Tours.FirstOrDefault(t => t.TourId == TourId);
            db.Tours.Remove(TourToDelete);
            db.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }
//<~~ E N D   O F   M A I N   V I E W S ~~> //
    }
}