using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
// v~~ S E T U P   V I E W ~~v //
// Context (delete ILogger)
        private ProdsAndCatsContext db;
        public HomeController(ProdsAndCatsContext context)
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
            return RedirectToAction("AllProducts");
        }
// All Products
        [HttpGet("products")]
        public IActionResult AllProducts()
        {
            List<Product> AllProducts = db.Products.ToList();
            ViewBag.AllProducts = AllProducts;
            return View("AllProducts");
        }
// Create Product in Db
        [HttpPost("create-product")]
        public IActionResult CreateProduct(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                db.Products.Add(newProduct);
                db.SaveChanges();
                return RedirectToAction("AllProducts"); 
            }
            return View("AllProducts", newProduct);
        }
// View Product
        [HttpGet("product/{prodId}")]
        public IActionResult ViewProduct(int prodId)
        {
            var thisProduct = db.Products
            // Include relationship name
            .Include(p => p.RelatedCategories)
            // Then Include the other relationship
            .ThenInclude(rel => rel.Category)
            .FirstOrDefault(p => p.ProductId == prodId);

            List<Category> theseCategories = db.Categories
                //List of Cats where related prods with this prod id is not ture
                .Where(c => c.RelatedProducts.Any(p => p.ProductId == prodId) == false)
                .ToList();
            ViewBag.Categories = theseCategories;

            return View("Product", thisProduct);
        }
// Add Category TO Product
        [HttpPost("add-cat-to-prod/{prodId}")]
        public IActionResult AddCatToProd(Relationship newRelationship, int prodId, int catId)
        {
            newRelationship.ProductId = prodId;
            newRelationship.CategoryId = catId;
            db.Relationships.Add(newRelationship);
            db.SaveChanges();
            return RedirectToAction("ViewProduct", prodId);
        }
// All Categories
        [HttpGet("categories")]
        public IActionResult AllCategories()
        {
            List<Category> AllCategories = db.Categories.ToList();
            ViewBag.AllCategories = AllCategories;
            return View("AllCategories");
        }
// Create Category in Db
        [HttpPost("create-category")]
        public IActionResult CreateCategory(Category newCategory)
        {
            if(ModelState.IsValid)
            {
                db.Categories.Add(newCategory);
                db.SaveChanges();
                return RedirectToAction("AllCategories");
            }
            return View("AllCategories", newCategory);
        }
// View Category
        [HttpGet("category/{catId}")]
        public IActionResult ViewCategory(int catId)
        {
            var thisCategory = db.Categories
            // Include the relationship to other model
            .Include(c => c.RelatedProducts)
            // Then Include the other relationship
            .ThenInclude(rel => rel.Product)
            .FirstOrDefault(c => c.CategoryId == catId);

            List<Product> theseProducts = db.Products
                //List of Prods where related cats with this cat id is not ture
                .Where(p => p.RelatedCategories.Any(c => c.CategoryId == catId) == false)
                .ToList();
            ViewBag.Products = theseProducts;

            return View("Category", thisCategory);
        }
// Add Product TO Category
        [HttpPost("add-prod-to-cat/{catId}")]
        public IActionResult AddToProdToCat(Relationship newRelationship, int catId, int prodId)
        {
            newRelationship.CategoryId = catId;
            newRelationship.ProductId = prodId;
            db.Relationships.Add(newRelationship);
            db.SaveChanges();
            return RedirectToAction("ViewCategory", catId);
        }

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
