using System;
using System.Collections.Generic;
using System.Linq;
using MapProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost("add-leg/{TourId}")]
        public IActionResult AddLeg(Leg newLeg, int TourId)
        {
            if (ModelState.IsValid)
            {
                newLeg.UserId = (int)uid;
                newLeg.TourId = TourId;

                // Metric -> Imperial Conversions
                double DistanceKm = newLeg.Distance * 1.609344;
                double VerticalM = newLeg.Vertical * 0.304800609601;

                // Munter Time Calculation
                newLeg.Time = (DistanceKm + (VerticalM / 100)) / newLeg.MunterRate;

                db.Legs.Add(newLeg);
                db.SaveChanges();
                return RedirectToAction("TourDetails"); 
            }
            return View("TourDetails", newLeg);
        }
    // Delete Leg
        [HttpGet("delete/leg/{LegId}")]
        public IActionResult DeleteLeg(int LegId)
        {
            Leg LegToDelete = db.Legs.FirstOrDefault(l => l.LegId == LegId);
            db.Legs.Remove(LegToDelete);
            db.SaveChanges();
            return RedirectToAction("TourDetails", "Tour");
        }
//<~~ E N D   O F   M A I N   V I E W S ~~> //
    }
}