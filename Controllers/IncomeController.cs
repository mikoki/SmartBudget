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
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;

namespace SmartBudget.Controllers
{
    public class IncomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IncomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        private User GetUserByUsername()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            return user;
        }


        private void PopulateIncomeTypeDropDownList(object selectedIncomeType = null)
        {
            User user = GetUserByUsername();
            var incomeTypeQuery = _db.IncomeTypes.Where(e => e.UserId == user.Id || String.IsNullOrEmpty(e.UserId.ToString()));
            ViewBag.IncomeTypeId = new SelectList(incomeTypeQuery, "Id", "Type", selectedIncomeType);
        }


        [Authorize]
        public IActionResult Index(string sortOrderIncome, string fromIncomes, string toIncomes, IncomeType incomeType = null)
        {
            if (String.IsNullOrEmpty(sortOrderIncome))
            {
                sortOrderIncome = "";
            }

            ViewData["TitleSortParm"] = sortOrderIncome == "Title" ? "title_desc" : "Title";
            ViewData["AmountSortParm"] = sortOrderIncome == "Amount" ? "amount_desc" : "Amount";
            ViewData["TypeSortParm"] = sortOrderIncome == "Type" ? "type_desc" : "Type";
            ViewData["DateSortParm"] = sortOrderIncome == "Date" ? "date_desc" : "Date";

            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);
            string firstDayOfMonth = startDate.ToString("yyyy-MM-dd");
            string today = now.ToString("yyyy-MM-dd");


            if (string.IsNullOrEmpty(fromIncomes)) { fromIncomes = firstDayOfMonth; }
            if (string.IsNullOrEmpty(toIncomes)) { toIncomes = today; }

            if (DateTime.Parse(toIncomes) < DateTime.Parse(fromIncomes))
            {
                TempData["ErrorExpenseDate"] = "To date cannot be less than from date. Returning values for current month";
                fromIncomes = firstDayOfMonth;
                toIncomes = today;
            }

            User user = GetUserByUsername();
            IncomeIndexData viewModel = new IncomeIndexData();

            switch (sortOrderIncome)
            {
                case "title_desc":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderByDescending(e => e.Title);
                    break;
                case "Title":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderBy(e => e.Title);
                    break;
                case "amount_desc":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderByDescending(e => e.Amount);
                    break;
                case "Amount":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderBy(e => e.Amount);
                    break;
                case "type_desc":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderByDescending(e => e.IncomeType.Type);
                    break;
                case "Type":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderBy(e => e.IncomeType.Type);
                    break;
                case "date_desc":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderByDescending(e => e.CreatedAt);
                    break;
                case "Date":
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderBy(e => e.CreatedAt);
                    break;
                default:
                    viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType).Where(e => e.UserId == user.Id).Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes)).Where(e => e.CreatedAt <= DateTime.Parse(toIncomes)).OrderByDescending(i => i.CreatedAt).ThenByDescending(i => i.Id);
                    break;
            }//used for sorting by title, type, amount and date of creation

            viewModel.IncomesType = _db.IncomeTypes.Where(e => e.UserId == user.Id || String.IsNullOrEmpty(e.UserId.ToString()));

            if (incomeType != null)
            {
                viewModel.IncomeType = incomeType;
            }

            TempData["FromIncomes"] = fromIncomes.ToString();
            TempData["ToIncomes"] = toIncomes.ToString();

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> IncomeTypeEdit(int? id)
        {
            if (!_db.IncomeTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            IncomeType incomeType = await _db.IncomeTypes.FindAsync(id);

            if (user.Id == incomeType.UserId)
            {
                return View(incomeType);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        public IActionResult IncomeTypeDelete(int? id)
        {
            if (!_db.IncomeTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            IncomeType incomeType = _db.IncomeTypes.Find(id);

            if (user.Id == incomeType.UserId)
            {
                return View(incomeType);
            }
            else
            {
                return Forbid();
            }

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IncomeTypeCreate(IncomeType incomeType)
        {
            User user = GetUserByUsername();
            incomeType.UserId = user.Id;
            if ((_db.IncomeTypes.Any(obj => obj.Type == incomeType.Type && obj.UserId == user.Id)))
            {
                TempData["DuplicateIncomeType"] = "Income type already exists.";
                return RedirectToAction("Index", incomeType);
            }
            if (ModelState.IsValid)
            {
                _db.IncomeTypes.Add(incomeType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["NotValid"] = "Income type is required field.";
            return RedirectToAction("Index", incomeType);
        }

        [Authorize]
        [HttpPost, ActionName("IncomeTypeEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IncomeTypeEditAction(int? id)
        {
            if (!_db.IncomeTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            IncomeType incomeTypeToUpdate = await _db.IncomeTypes.SingleOrDefaultAsync(s => s.Id == id);

            if (user.Id != incomeTypeToUpdate.UserId)
            {
                return Forbid();
            }


            if (await TryUpdateModelAsync<IncomeType>(incomeTypeToUpdate, "", e => e.Type))
            {
                if ((_db.IncomeTypes.Any(obj => obj.Type == incomeTypeToUpdate.Type && obj.UserId == user.Id)))
                {
                    TempData["DuplicateIncomeType"] = "Income type already exists.";
                    return View(incomeTypeToUpdate);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            TempData["IncomeTypeEditWarning"] = "Income type is not changed. Please provide valid information.";
            return View(incomeTypeToUpdate);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncomeTypeDelete(int id)
        {
            if (!_db.IncomeTypes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            IncomeType incomeType = _db.IncomeTypes.Find(id);

            if (user.Id != incomeType.UserId)
            {
                return Forbid();
            }

            _db.IncomeTypes.Remove(incomeType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult IncomeCreate()
        {
            Income income = new Income();
            income.CreatedAt = DateTime.Now;
            PopulateIncomeTypeDropDownList();
            return View(income);
        }

        [Authorize]
        public IActionResult IncomeEdit(int? id)
        {
            if (!_db.Incomes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Income income = _db.Incomes.Find(id);

            if (user.Id == income.UserId)
            {
                PopulateIncomeTypeDropDownList(income.IncomeTypeId);
                return View(income);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        public IActionResult IncomeDelete(int? id)
        {
            if (!_db.Incomes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Income income = _db.Incomes.Find(id);

            if (user.Id == income.UserId)
            {
                return View(income);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IncomeCreate(Income income)
        {
            income.Amount = Convert.ToDecimal(income.Amount);
            User user = GetUserByUsername();
            income.UserId = user.Id;
            if (ModelState.IsValid)
            {
                _db.Incomes.Add(income);
                _db.SaveChanges();
                return Redirect("Index");
            }
            PopulateIncomeTypeDropDownList(income.IncomeTypeId);
            return View(income);
        }

        [Authorize]
        [HttpPost, ActionName("IncomeEdit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IncomeEditAction(int? id)
        {
            if (!_db.Incomes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Income incomeToUpdate = _db.Incomes.Find(id);
            if (user.Id != incomeToUpdate.UserId)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Income>(incomeToUpdate, "", e => e.Title, e => e.Amount, e => e.IncomeTypeId, e => e.CreatedAt))
            {
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateIncomeTypeDropDownList(incomeToUpdate.IncomeTypeId);
            TempData["IncomeEditWarning"] = "Income is not changed. Please provide valid information.";
            return View(incomeToUpdate);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncomeDelete(int id)
        {
            if (!_db.Incomes.Any(obj => obj.Id == id))
            {
                return NotFound();
            }

            User user = GetUserByUsername();
            Income income = _db.Incomes.Find(id);

            if (user.Id != income.UserId)
            {
                return Forbid();
            }

            _db.Incomes.Remove(income);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public void IncomeReport(string fromIncomes, string toIncomes)
        {
            User user = GetUserByUsername();
            IncomeIndexData viewModel = new IncomeIndexData();
            viewModel.Incomes = _db.Incomes.Include(i => i.IncomeType).Where(i => i.UserId == user.Id).Where(i => i.CreatedAt >= DateTime.Parse(fromIncomes)).Where(i => i.CreatedAt <= DateTime.Parse(toIncomes)).OrderByDescending(i => i.CreatedAt).ThenByDescending(i => i.Id);
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Title";
            ws.Cells["B1"].Value = "Income report";

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = String.Format("From {0:dd MMMM yyyy} to {1:dd MMMM yyyy}", DateTime.Parse(fromIncomes), DateTime.Parse(toIncomes));

            ws.Cells["A6"].Value = "Title";
            ws.Cells["B6"].Value = "Amount";
            ws.Cells["C6"].Value = "Type";
            ws.Cells["D6"].Value = "Date of payment";

            int rowStart = 7;

            foreach (Income income in viewModel.Incomes)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = income.Title;
                ws.Cells[string.Format("B{0}", rowStart)].Value = income.Amount.ToString();
                ws.Cells[string.Format("C{0}", rowStart)].Value = income.IncomeType.Type;
                ws.Cells[string.Format("D{0}", rowStart)].Value = String.Format("{0:dd MMMM yyyy}", income.CreatedAt);
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("content-disposition", "attachment: filename=" + "ReportExpensesDetails.xlsx");
            Response.Body.WriteAsync(pck.GetAsByteArray());
        }
    }
}