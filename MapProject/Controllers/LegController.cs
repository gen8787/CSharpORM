using MapProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MapProject.Controllers
{
    public class LegController : Controller
    {
// v~~ S E T U P   V I E W S ~~v //
    // Context (delete ILogger)
        private MapProjectContext db;
        public LegController(MapProjectContext context)
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
    // Create Leg -> Add to Db
        [HttpPost("add-leg")]
        public IActionResult AddLeg(Leg newLeg)
        {
            if (ModelState.IsValid)
            {
                newLeg.UserId = (int)uid;

                // t = (d + (v / 100)) / r
                newLeg.Time = (newLeg.Distance + (newLeg.Vertical / 100)) / newLeg.MunterRate;

                db.Legs.Add(newLeg);
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
            return View("Index", newLeg);
        }
//<~~ E N D   O F   M A I N   V I E W S ~~> //
    }
}