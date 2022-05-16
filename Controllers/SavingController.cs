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
    public class SavingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SavingController(ApplicationDbContext db)
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
            IEnumerable<Saving> saving = _db.Savings.Include(s => s.User).Where(e => e.UserId == user.Id);
            return View(saving);
        }

        [Authorize]
        public IActionResult SavingCreate()
        {
            return View();
        }

        [Authorize]
        public IActionResult SavingDetails(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            User user = GetUserByUsername();
            SavingSavingLogs viewModel = new SavingSavingLogs();
            viewModel.Saving = _db.Savings.Include(s => s.User).Where(s => s.Id == id).First();
            if (viewModel.Saving == null)
            {
                return NotFound();
            }
            if (viewModel.Saving.UserId != user.Id)
            {
                return Forbid();
            }
            viewModel.SavingLogs = _db.SavingLogs.Include(s => s.Saving).Where(s => s.SavingId == viewModel.Saving.Id);
            return View(viewModel);
        }

        [Authorize]
        public IActionResult SavingDelete(int? id)
        {
            if (!_db.Savings.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Saving saving = _db.Savings.Find(id);

            if (user.Id == saving.UserId)
            {
                return View(saving);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavingCreate(Saving saving)
        {
            User user = GetUserByUsername();
            saving.UserId = user.Id;
            saving.CreatedAt = DateTime.Now;
            saving.CurrentAmount = Convert.ToDecimal(0);
            if (ModelState.IsValid)
            {
                _db.Savings.Add(saving);
                _db.SaveChanges();
                return Redirect("Index");
            }
            return View(saving);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WithdrawAmount(int? id, SavingLog savingLog)
        {
            if (savingLog.Amount <= 0)
            {
                TempData["SavingError"] = "Please provide positive amount";
            }
            if (String.IsNullOrEmpty(savingLog.Amount.ToString()))
            {
                TempData["SavingError"] = "Amount field is required";
            }
            if (id == null)
            {
                return BadRequest();
            }
            Saving saving = _db.Savings.Where(s => s.Id == id).First();
            if (saving == null)
            {
                return NotFound();
            }
            User user = GetUserByUsername();
            if (saving.UserId != user.Id)
            {
                return Forbid();
            }

            if (saving.CurrentAmount - savingLog.Amount < 0)
            {
                TempData["WithdrawError"] = "Operation cancelled. Insufficient balance.";
                return RedirectToAction("SavingDetails", new { id = saving.Id });
            }
            savingLog.SavingId = saving.Id;
            savingLog.Type = SavingLog.ExchangeType.Withdraw;
            savingLog.UpdatedAt = DateTime.Now;
            savingLog.Amount = Convert.ToDecimal(savingLog.Amount);
            if (ModelState.IsValid)
            {
                _db.SavingLogs.Add(savingLog);
                saving.CurrentAmount = saving.CurrentAmount - savingLog.Amount;
                _db.SaveChanges();
                return RedirectToAction("SavingDetails", new { id = saving.Id });
            }

            return RedirectToAction("SavingDetails", new { id = saving.Id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAmount(int? id, SavingLog savingLog)
        {
            if (savingLog.Amount <= 0)
            {
                TempData["SavingError"] = "Please provide positive amount";
            }
            if (String.IsNullOrEmpty(savingLog.Amount.ToString()))
            {
                TempData["SavingError"] = "Amount field is required";
            }

            if (id == null)
            {
                return BadRequest();
            }
            Saving saving = _db.Savings.Where(s => s.Id == id).First();
            if (saving == null)
            {
                return NotFound();
            }
            User user = GetUserByUsername();
            if (saving.UserId != user.Id)
            {
                return Forbid();
            }

            savingLog.SavingId = saving.Id;
            savingLog.Type = SavingLog.ExchangeType.Save;
            savingLog.UpdatedAt = DateTime.Now;
            savingLog.Amount = Convert.ToDecimal(savingLog.Amount);
            if (ModelState.IsValid)
            {
                if (saving.CurrentAmount + savingLog.Amount > saving.AmountGoal)
                {
                    savingLog.Amount = saving.AmountGoal - saving.CurrentAmount;
                }

                saving.CurrentAmount = saving.CurrentAmount + savingLog.Amount;
                if (saving.CurrentAmount == saving.AmountGoal)
                {
                    saving.IsCompleted = true;
                }
                _db.SavingLogs.Add(savingLog);
                _db.SaveChanges();
                return RedirectToAction("SavingDetails", new { id = saving.Id });
            }
            return RedirectToAction("SavingDetails", new { id = saving.Id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavingDelete(int id)
        {
            if (!_db.Savings.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Saving saving = _db.Savings.Find(id);

            if (user.Id != saving.UserId)
            {
                return Forbid();
            }

            _db.Savings.Remove(saving);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
