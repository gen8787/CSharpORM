﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankAccounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
// v~~ M A I N   V I E W S ~~v //
// Context (delete ILogger)
        private BankAccountsContext db;
        public HomeController(BankAccountsContext context)
        {
            db = context;
        }
// Index Page - Register Form
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }
// Register - Create User
        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered, please login.");
                    return View("Index", newUser);
                }
                Microsoft.AspNetCore.Identity.PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                db.Users.Add(newUser);
                db.SaveChanges();
                HttpContext.Session.SetInt32("userId", newUser.UserId);
                HttpContext.Session.SetString("userFirstName", newUser.FirstName);
                return RedirectToAction("Account", newUser.UserId); 
            }
            return View("Index", newUser);
        }
// Login Page - Login Form
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }
// Login - Validate User
        [HttpPost("process-login")]
        public IActionResult ProcessLogin(LoginUser loginUser)
        {
            if(ModelState.IsValid)
            {
                var userInDb = db.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid email address. Please try again or register.");
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();            
                var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid passowrd. Please try again or register.");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("userId", userInDb.UserId);
                HttpContext.Session.SetString("userFirstName", userInDb.FirstName);
                return RedirectToAction("Account", userInDb.UserId); 
            }
            return View("Login");
        }
// Account Page
        [HttpGet("account")]
        public IActionResult Account()
        {
            if(HttpContext.Session.GetInt32("userId") == null)
            {
                return RedirectToAction("Index");
            }
            User currentUser = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
            List<Transaction> allTransactions = db.Transactions
                .Where(t => t.UserId == currentUser.UserId)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
                
            ViewBag.allTransactions = allTransactions;

            decimal? balance = db.Transactions
                .Where(t => t.UserId == currentUser.UserId)
                .Sum(t => t.Amount);
            if (balance == null)
            {
                balance = (decimal?)0.00;
            }
            ViewBag.Balance = Math.Round((decimal)balance,2);
            string Balance = ViewBag.Balance.ToString();
            HttpContext.Session.SetString("userBal", Balance);
            return View("Account");
        }
// Create Transaction
        [HttpPost("create-transaction")]
        public IActionResult CreateTransaction(Transaction newTransaction)
        {
            if(ModelState.IsValid)
            {
                string Balance = HttpContext.Session.GetString("userBal");
                decimal decBalance = Convert.ToDecimal(Balance);
                if (newTransaction.Amount < 0)
                {
                    if (decBalance + newTransaction.Amount <= 0)
                    {
                        return RedirectToAction("Account");
                    }
                }
                newTransaction.UserId = (int)HttpContext.Session.GetInt32("userId");
                db.Transactions.Add(newTransaction);
                db.SaveChanges();
                return RedirectToAction("Account"); 
            }
            return View("CreateTransaction", newTransaction);
        }
// Logout
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
