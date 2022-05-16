using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBudget.Data;
using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SmartBudget.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ApplicationDbContext _db;
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult LoginPage(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public bool CheckHashedPasswors(string passwordFromDB, string inputPassword)
        {
            byte[] hashedBytes = Convert.FromBase64String(passwordFromDB);
            byte[] salt = new byte[16];
            Array.Copy(hashedBytes, 0, salt, 0, 16);
            //hash the password from the user's input
            var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            bool check = true;

            //i + 16 since the first 16 elements in hashedBytes are salt
            for (int i = 0; i < 20; i++)
            {
                if (hashedBytes[i + 16] != hash[i])
                {
                    check = false;
                }
            }

            return check;
        }
        public async Task<IActionResult> Validate(string username, string password, string returnUrl)
        {
            bool userExists = false;
            userExists = _db.Users.Any(c => c.Username == username);
            if (userExists)
            {
                User user = new User();
                user = _db.Users.Include(user => user.Role).First(c => c.Username == username);//include is used in order to get role from Role table
                if (CheckHashedPasswors(user.Password, password))
                {
                    //claims are properties that describe user, for example:username
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Username));
                    claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); //create identity for the provided user
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity); //create authentication ticket for the provided identity
                    await HttpContext.SignInAsync(claimsPrincipal);
                    //information stored in cookie in order for the user to be identified
                    user.IsLogged = true;
                    _db.SaveChanges();
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect("/Home");
                    }//if user clicked log out button
                    return Redirect(returnUrl);
                }
            }
            TempData["Error"] = "Username or password is invalid.";
            return Redirect("LoginPage");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                user.IsLogged = false;
                _db.SaveChanges();
            }
            await HttpContext.SignOutAsync();
            return (Redirect("LoginPage"));
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
