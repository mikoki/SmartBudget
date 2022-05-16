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
    public class ReminderController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ReminderController(ApplicationDbContext db)
        {
            _db = db;
        }

        private User GetUserByUsername()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            return user;
        }

        [Authorize]
        public IActionResult Index()
        {
            User user = GetUserByUsername();
            IEnumerable<Reminder> reminders = _db.Reminders.Include(s => s.User).Where(r => r.UserId == user.Id);
            ReminderCategories reminderCategores = new ReminderCategories();
            reminderCategores.CurrentReminders = reminders.Where(r => r.DateOfReminding == DateTime.Today);
            reminderCategores.UpcomingReminders = reminders.Where(r => r.DateOfReminding > DateTime.Today);
            reminderCategores.PastReminders = reminders.Where(r => r.DateOfReminding < DateTime.Today);
            return View(reminderCategores);
        }

        [Authorize]
        public IActionResult ReminderCreate()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ReminderEdit(int? id)
        {
            if (!_db.Reminders.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Reminder reminder = await _db.Reminders.FindAsync(id);

            if (user.Id == reminder.UserId)
            {
                return View(reminder);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        public IActionResult ReminderDelete(int? id)
        {
            if (!_db.Reminders.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Reminder reminder = _db.Reminders.Find(id);

            if (user.Id == reminder.UserId)
            {
                return View(reminder);
            }
            else
            {
                return Forbid();
            }

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReminderCreate(Reminder reminder)
        {
            User user = GetUserByUsername();

            reminder.CreatedAt = DateTime.Now;
            reminder.UserId = user.Id;
            reminder.IsDisplayed = false;

            if (ModelState.IsValid)
            {
                _db.Reminders.Add(reminder);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reminder);
        }

        [Authorize]
        [HttpPost, ActionName("ReminderEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReminderAction(int? id)
        {
            if (!_db.Reminders.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Reminder reminderToUpdate = await _db.Reminders.SingleOrDefaultAsync(s => s.Id == id);

            if (user.Id != reminderToUpdate.UserId)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Reminder>(reminderToUpdate, "", r => r.Title, r => r.Description, r => r.DateOfReminding))
            {
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            TempData["ReminderEditWarning"] = "Reminder is not changed. Please provide valid information.";
            return View(reminderToUpdate);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReminderDelete(int id)
        {
            if (!_db.Reminders.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Reminder reminder = _db.Reminders.Find(id);

            if (user.Id != reminder.UserId)
            {
                return Forbid();
            }

            _db.Reminders.Remove(reminder);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReminderDone(int? id)
        {
            if (!_db.Reminders.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Reminder reminderToUpdate = await _db.Reminders.SingleOrDefaultAsync(s => s.Id == id);
           
            if (user.Id != reminderToUpdate.UserId)
            {
                return Forbid();
            }
            if (ModelState.IsValid)
            {
                reminderToUpdate.IsDisplayed = true;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
