using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartBudget.Data;
using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartBudget.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SmartBudget.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }


        private User GetUserByUsername()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            return user;
        }


        private void PopulateExpenseTypeDropDownList(object selectedExpenseType = null)
        {
            User user = GetUserByUsername();
            var expenseTypeQuery = _db.ExpenseTypes.Where(e => e.UserId == user.Id || String.IsNullOrEmpty(e.UserId.ToString()));
            ViewBag.ExpenseTypeId = new SelectList(expenseTypeQuery, "Id", "Type", selectedExpenseType);
        }


        [Authorize]
        public IActionResult Index(string sortOrderExpense, ExpenseType expenseType = null)
        {
            if (String.IsNullOrEmpty(sortOrderExpense))
            {
                sortOrderExpense = "";
            }

            ViewData["TitleSortParm"] = sortOrderExpense == "Title" ? "title_desc" : "Title";
            ViewData["AmountSortParm"] = sortOrderExpense == "Amount" ? "amount_desc" : "Amount";
            ViewData["TypeSortParm"] = sortOrderExpense == "Type" ? "type_desc" : "Type";
            ViewData["DateSortParm"] = sortOrderExpense == "Date" ? "date_desc" : "Date";


            User user = GetUserByUsername();
            ExpenseIndexData viewModel = new ExpenseIndexData();

            switch (sortOrderExpense)
            {
                case "title_desc":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderByDescending(e => e.Title);
                    break;
                case "Title":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderBy(e => e.Title);
                    break;
                case "amount_desc":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderByDescending(e => e.Amount);
                    break;
                case "Amount":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderBy(e => e.Amount);
                    break;
                case "type_desc":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderByDescending(e => e.ExpenseType.Type);
                    break;
                case "Type":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderBy(e => e.ExpenseType.Type);
                    break;
                case "date_desc":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderByDescending(e => e.CreatedAt);
                    break;
                case "Date":
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderBy(e => e.CreatedAt);
                    break;
                default:
                    viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType).Where(e => e.UserId == user.Id).OrderByDescending(i => i.CreatedAt).ThenByDescending(i => i.Id);
                    break;
            }//used for sorting by title and date of creation

            viewModel.ExpensesType = _db.ExpenseTypes.Where(e => e.UserId == user.Id || String.IsNullOrEmpty(e.UserId.ToString()));

            if (expenseType != null)
            {
                viewModel.ExpenseType = expenseType;
            }
     
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> ExpenseTypeEdit(int? id)
        {
            if (!_db.ExpenseTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            ExpenseType expenseType = await _db.ExpenseTypes.FindAsync(id);

            if (user.Id == expenseType.UserId)
            {
                return View(expenseType);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        public IActionResult ExpenseTypeDelete(int? id)
        {
            if (!_db.ExpenseTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            ExpenseType expenseType = _db.ExpenseTypes.Find(id);

            if (user.Id == expenseType.UserId)
            {
                return View(expenseType);
            }
            else
            {
                return Forbid();
            }

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExpenseTypeCreate(ExpenseType expenseType)
        {
            User user = GetUserByUsername();
            expenseType.UserId = user.Id;
            if ((_db.ExpenseTypes.Any(obj => obj.Type == expenseType.Type && obj.UserId == user.Id)))
            {
                TempData["DuplicateExpenseType"] = "Expense type already exists.";
                return RedirectToAction("Index", expenseType);
            }
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Add(expenseType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["NotValid"] = "Expense type is required field.";
            return RedirectToAction("Index", expenseType);
        }

        [Authorize]
        [HttpPost, ActionName("ExpenseTypeEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExpenseTypeEditAction(int? id)
        {
            if (!_db.ExpenseTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            ExpenseType expenseTypeToUpdate = await _db.ExpenseTypes.SingleOrDefaultAsync(s => s.Id == id);

            if (user.Id != expenseTypeToUpdate.UserId)
            {
                return Forbid();
            }


            if (await TryUpdateModelAsync<ExpenseType>(expenseTypeToUpdate, "", e => e.Type))
            {
                if ((_db.ExpenseTypes.Any(obj => obj.Type == expenseTypeToUpdate.Type && obj.UserId == user.Id)))
                {
                    TempData["DuplicateExpenseType"] = "Expense type already exists.";
                    return View(expenseTypeToUpdate);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            TempData["ExpenseTypeEditWarning"] = "Expense type is not changed. Please provide valid information.";
            return View(expenseTypeToUpdate);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExpenseTypeDelete(int id)
        {
            if (!_db.ExpenseTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            ExpenseType expenseType = _db.ExpenseTypes.Find(id);

            if (user.Id != expenseType.UserId)
            {
                return Forbid();
            }

            _db.ExpenseTypes.Remove(expenseType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult ExpenseCreate()
        {
            Expense expense = new Expense();
            expense.CreatedAt = DateTime.Now;
            PopulateExpenseTypeDropDownList();
            return View(expense);
        }

        [Authorize]
        public IActionResult ExpenseEdit(int? id)
        {
            if (!_db.Expenses.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Expense expense = _db.Expenses.Find(id);

            if (user.Id == expense.UserId)
            {
                PopulateExpenseTypeDropDownList(expense.ExpenseTypeId);
                return View(expense);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        public IActionResult ExpenseDelete(int? id)
        {
            if (!_db.Expenses.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Expense expense = _db.Expenses.Find(id);

            if (user.Id == expense.UserId)
            {
                return View(expense);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExpenseCreate(Expense expense)
        {
            expense.Amount = Convert.ToDecimal(expense.Amount);
            User user = GetUserByUsername();
            expense.UserId = user.Id;
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(expense);
                _db.SaveChanges();
                return Redirect("Index");
            }
            PopulateExpenseTypeDropDownList(expense.ExpenseTypeId);
            return View(expense);
        }

        [Authorize]
        [HttpPost, ActionName("ExpenseEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExpenseEditAction(int? id)
        {
            if (!_db.Expenses.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Expense expenseToUpdate = _db.Expenses.Find(id);
            if (user.Id != expenseToUpdate.UserId)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Expense>(expenseToUpdate, "", e => e.Title, e => e.Amount, e => e.ExpenseTypeId, e => e.CreatedAt))
            {
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateExpenseTypeDropDownList(expenseToUpdate.ExpenseTypeId);
            TempData["ExpenseEditWarning"] = "Expense is not changed. Please provide valid information.";
            return View(expenseToUpdate);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExpenseDelete(int id)
        {
            if (!_db.Expenses.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Expense expense = _db.Expenses.Find(id);

            if (user.Id != expense.UserId)
            {
                return Forbid();
            }

            _db.Expenses.Remove(expense);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}