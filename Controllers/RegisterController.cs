using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBudget.Data;
using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SmartBudget.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public string HashPassword(string password)
        {
            byte[] salt; //used in hashing
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashedBytes = new byte[36];

            Array.Copy(salt, 0, hashedBytes, 0, 16); //place the salt in the array, from 0 to 15
            Array.Copy(hash, 0, hashedBytes, 16, 20); //place the hash in the array, from 16 to 35
            return Convert.ToBase64String(hashedBytes);
        }
        
        public IActionResult Index()
        {
            return View();
        }
        //get register
        public IActionResult register()
        {
            return View();
        }

        //post register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult register(User user)
        {
            if ((_db.Users.Any(obj => obj.Username == user.Username)) || (_db.Users.Any(obj => obj.Email == user.Email))){
                if (_db.Users.Any(obj => obj.Username == user.Username))
                {
                    TempData["DuplicateUsername"] = "Username is already taken.";
                }
                if (_db.Users.Any(obj => obj.Email == user.Email))
                {
                    TempData["DuplicateEmail"] = "User with this email already exists.";
                }
                return View(user);
            }
            if (ModelState.IsValid)
            {
                Role role = _db.Roles.Where(obj => obj.RoleName == "User").FirstOrDefault();
                user.Password = HashPassword(user.Password);
                user.Role = role;
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["Success"] = "Registration successful. Please enter your credentials.";
                return Redirect("/Login/LoginPage");
            }
            else
            {
                return View(user);
            }
        }
    }
}
