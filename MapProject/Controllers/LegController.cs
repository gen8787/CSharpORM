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
    // Tour Planner Page -> View & Add Legs
        [HttpGet("tour-planner")]
        public IActionResult TourPlanner()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Leg> AllLegs = db.Legs
            .ToList();
            ViewBag.AllLegs = AllLegs;

            double totalDist = db.Legs
                .Sum(d => d.Distance);
            ViewBag.totalDist = Math.Round(totalDist, 2);

            double totalVert = db.Legs
                .Sum(v => v.Vertical);
            ViewBag.totalVert = Math.Round(totalVert, 2);

            double totalTime = db.Legs
                .Sum(t => t.Time);
            if (totalTime > 1)
            {
                double totalTimeHr = Math.Floor(totalTime);
                double totalTimeMin = Math.Round((totalTime - Math.Floor(totalTime)) * 60);
                ViewBag.totalTimeHr = totalTimeHr;
                ViewBag.totalTimeMin = totalTimeMin;
            }
            else
            {
                double totalTimeMin = Math.Round(totalTime * 60);
                ViewBag.totalTimeMin = totalTimeMin;
            }
            return View("TourPlanner");
        }
    // Create Leg -> Add to Db
        [HttpPost("add-leg")]
        public IActionResult AddLeg(Leg newLeg)
        {
            if (ModelState.IsValid)
            {
                newLeg.UserId = (int)uid;
                double DistanceKm = newLeg.Distance * 1.609344;
                double VerticalM = newLeg.Vertical * 0.304800609601;

                // t = (d + (v / 100)) / r
                newLeg.Time = (DistanceKm + (VerticalM / 100)) / newLeg.MunterRate;
                // newLeg.Distance = DistanceKm;
                // newLeg.Vertical = VerticalM;

                db.Legs.Add(newLeg);
                db.SaveChanges();
                return RedirectToAction("TourPlanner"); 
            }
            return View("TourPlanner", newLeg);
        }
    // Delete Leg
        [HttpGet("delete/{LegId}")]
        public IActionResult Delete(int LegId)
        {
            Leg LegToDelete = db.Legs.FirstOrDefault(l => l.LegId == LegId);
            db.Legs.Remove(LegToDelete);
            db.SaveChanges();
            return RedirectToAction("TourPlanner");
        }
//<~~ E N D   O F   M A I N   V I E W S ~~> //
    }
}