using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBudget.Data;
using SmartBudget.Models;
using SmartBudget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartBudget.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        private User GetUserByUsername()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Where(u => u.Username == username).Where(u => u.Role.RoleName == "Admin").FirstOrDefault();
            return user;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            User user = GetUserByUsername();
            AdminUsers viewModel = new AdminUsers();
            Console.WriteLine(searchString);
            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Users = _db.Users.Include(u => u.Role).Where(u => u.Username != user.Username).Where(u => u.Username.Contains(searchString)).OrderBy(u => u.Username);
            }
            else
            {
                viewModel.Users = _db.Users.Include(u => u.Role).Where(u => u.Username != user.Username).OrderBy(u => u.Username);
            }

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserDetails(int? id)
        {
            if (!_db.Users.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            User userDetails = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);

            return View(userDetails);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnblockUser(int id)
        {
            if (!_db.Users.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            User userToUpdate = _db.Users.Find(id);
            userToUpdate.isBlocked = false;
            _db.SaveChanges();
            return RedirectToAction("UserDetails", new { id = id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlockUser(int id)
        {
            if (!_db.Users.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            User userToUpdate = _db.Users.Find(id);
            userToUpdate.isBlocked = true;
            _db.SaveChanges();
            return RedirectToAction("UserDetails", new { id = id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeAdmin(int id)
        {
            if (!_db.Users.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            User userToUpdate = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            Role roleToUpdate = _db.Roles.Where(r => r.RoleName == "Admin").FirstOrDefault();
            userToUpdate.RoleId = roleToUpdate.Id;
            await _db.SaveChangesAsync();
            return RedirectToAction("UserDetails", new { id = id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeUser(int id)
        {
            if (!_db.Users.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            User userToUpdate = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            Role roleToUpdate = _db.Roles.Where(r => r.RoleName == "User").FirstOrDefault();
            userToUpdate.RoleId = roleToUpdate.Id;
            await _db.SaveChangesAsync();
            return RedirectToAction("UserDetails", new { id = id });
        }
    }
}
