using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
// v~~ M A I N   V I E W S ~~v //
// Context (delete ILogger)
        private ChefsNDishesContext db;
        public HomeController(ChefsNDishesContext context)
        {
            db = context;
        }
// Index Page - All Chefs
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> AllChefs = db.Chefs
            .Include(c => c.CreatedDishes)
            // Chef has a list of created dishes to relate to Dish Model
            .ToList();

            return View("Index", AllChefs);
        }
// Add Chef Page
        [HttpGet("add-chef")]
        public IActionResult AddChef()
        {
            return View("CreateChef");
        }
// Create Chef in DB
        [HttpPost("create-chef")]
        public IActionResult CreateChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                db.Chefs.Add(newChef);
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
            return View("CreateChef", newChef);
        }
// All Dishes Page
        [HttpGet("dishes")]
        public IActionResult AllDishes()
        {
            List<Dish> AllDishes = db.Dishes
            .Include(d => d.Creator)
            // Dish has a creator to relate to the Chef Model
            .ToList();

            return View("AllDishes", AllDishes);
        }
// Add Dish Page
        [HttpGet("add-dish")]
        public IActionResult AddDish()
        {
            List<Chef> AllChefs = db.Chefs.ToList();
            ViewBag.AllChefs = AllChefs;
            return View("CreateDish");
        }
// Create Dish in DB
        [HttpPost("create-dish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                db.Dishes.Add(newDish);
                db.SaveChanges();
                return RedirectToAction("AllDishes"); 
            }
            return View("CreateDish", newDish);
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
