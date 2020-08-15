using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
// v~~ M A I N   V I E W S ~~v //
// Context (delete ILogger)
        private CrudContext dbContext;
        public HomeController(CrudContext context)
        {
            dbContext = context;
        }
// Index - All Dishes
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes
            .OrderByDescending(dish => dish.CreatedAt)
            .ToList();

            return View("Index", AllDishes);
        }
// Add Dish Page
        [HttpGet("add-dish")]
        public IActionResult AddDish()
        {
            return View("CreateDish");
        }
// Create Dish in DB from Form
        [HttpPost("create-dish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Dishes.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index"); 
            }
            return View("CreateDish", newDish);
        }
// View Dish
        [HttpGet("{dishID}")]
        public IActionResult ViewDish(int dishId)
        {
            Dish dishToView = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View("ViewDish", dishToView);
        }
// Edit Dish Page
        [HttpPost("edit-dish/{dishID}")]
        public IActionResult Edit(int dishId)
        {
            Dish dishToEdit = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View("EditDish", dishToEdit);
        }
// Update Dish in DB
        [HttpPost("update-dish/{dishId}")]
        public IActionResult UpdateDish(Dish dishFromForm, int dishId)
        {
            if (ModelState.IsValid)
            {
                Dish dishToUpdate = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
                dishToUpdate.Name = dishFromForm.Name;
                dishToUpdate.Chef = dishFromForm.Chef;
                dishToUpdate.Tastiness = dishFromForm.Tastiness;
                dishToUpdate.Calories = dishFromForm.Calories;
                dishToUpdate.Description = dishFromForm.Description;
                dishToUpdate.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("ViewDish", dishId);
            }
            return View("EditDish", dishFromForm);
        }
// Delete Dish from DB
        [HttpPost("delete-dish/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish dishToDelete = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            dbContext.Dishes.Remove(dishToDelete);
            dbContext.SaveChanges();
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
