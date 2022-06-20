using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SmartBudget.Data;
using SmartBudget.Models;
using SmartBudget.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace SmartBudget.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        private readonly ApplicationDbContext _db;

        private User GetUserByUsername()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            return user;
        }

        [Authorize]
        public IActionResult Index(string fromExpenses, string toExpenses, string fromIncomes, string toIncomes)
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);
            string firstDayOfMonth = startDate.ToString("yyyy-MM-dd");
            string today = now.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(fromExpenses)) {fromExpenses = firstDayOfMonth;}
            if (string.IsNullOrEmpty(toExpenses)) { toExpenses = today; }
            if (string.IsNullOrEmpty(fromIncomes)) { fromIncomes = firstDayOfMonth; }
            if (string.IsNullOrEmpty(toIncomes)) { toIncomes = today; }

            if(DateTime.Parse(toExpenses) < DateTime.Parse(fromExpenses))
            {
                TempData["ErrorExpenseDate"] = "To date cannot be less than from date. Returning values for current month";
                fromExpenses = firstDayOfMonth;
                toExpenses = today;
            }

            if (DateTime.Parse(toIncomes) < DateTime.Parse(fromIncomes))
            {
                TempData["ErrorIncomeDate"] = "To date cannot be less than from date. Returning values for current month";
                fromIncomes = firstDayOfMonth;
                toIncomes = today;
            }

            TempData["FromExpenses"] = fromExpenses.ToString();
            TempData["ToExpenses"] = toExpenses.ToString();
            TempData["FromIncomes"] = fromIncomes.ToString();
            TempData["ToIncomes"] = toIncomes.ToString();

            User user = GetUserByUsername();
            HomeData viewModel = new HomeData();
            viewModel.User = user;
            viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType)
                .Where(e => e.UserId == user.Id)
                .Where(e => e.ExpenseType.UserId == user.Id || e.ExpenseType.UserId == null)
                .Where(e => e.CreatedAt >= DateTime.Parse(fromExpenses))
                .Where(e => e.CreatedAt <= DateTime.Parse(toExpenses))
                .GroupBy(e => e.ExpenseType.Type)
                .Select(e => new ExpenseAmount() { Type = e.Key, Amount = e.Sum(ex => ex.Amount) })
                .OrderByDescending(e => e.Amount);

            decimal totalExpenses = viewModel.Expenses.Sum(e => e.Amount);
            TempData["TotalExpenses"] = totalExpenses.ToString();

            viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType)
                .Where(e => e.UserId == user.Id)
                .Where(e => e.IncomeType.UserId == user.Id || e.IncomeType.UserId == null)
                .Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes))
                .Where(e => e.CreatedAt <= DateTime.Parse(toIncomes))
                .GroupBy(e => e.IncomeType.Type)
                .Select(e => new IncomeAmount() { Type = e.Key, Amount = e.Sum(ex => ex.Amount) })
                .OrderByDescending(e => e.Amount);

            decimal TotalIncomes = viewModel.Incomes.Sum(e => e.Amount);
            TempData["TotalIncomes"] = TotalIncomes.ToString();

            IEnumerable<Reminder> reminders = _db.Reminders.Include(s => s.User).Where(r => r.UserId == user.Id).Where(r => r.DateOfReminding == DateTime.Today).Where(r => r.IsDisplayed == false);
            TempData["Reminders"] = reminders.Count().ToString();
            return View(viewModel);
        }

        [Authorize]
        public void SummaryReport(string fromExpenses, string toExpenses, string fromIncomes, string toIncomes)
        {
            User user = GetUserByUsername();
            HomeData viewModel = new HomeData();
            viewModel.Expenses = _db.Expenses.Include(e => e.ExpenseType)
                .Where(e => e.UserId == user.Id)
                .Where(e => e.ExpenseType.UserId == user.Id || e.ExpenseType.UserId == null)
                .Where(e => e.CreatedAt >= DateTime.Parse(fromExpenses))
                .Where(e => e.CreatedAt <= DateTime.Parse(toExpenses))
                .GroupBy(e => e.ExpenseType.Type)
                .Select(e => new ExpenseAmount() { Type = e.Key, Amount = e.Sum(ex => ex.Amount) })
                .OrderByDescending(e => e.Amount);

            viewModel.Incomes = _db.Incomes.Include(e => e.IncomeType)
                .Where(e => e.UserId == user.Id)
                .Where(e => e.IncomeType.UserId == user.Id || e.IncomeType.UserId == null)
                .Where(e => e.CreatedAt >= DateTime.Parse(fromIncomes))
                .Where(e => e.CreatedAt <= DateTime.Parse(toIncomes))
                .GroupBy(e => e.IncomeType.Type)
                .Select(e => new IncomeAmount() { Type = e.Key, Amount = e.Sum(ex => ex.Amount) })
                .OrderByDescending(e => e.Amount);

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Title";
            ws.Cells["B1"].Value = "Report for expenses and incomes by categories.";
            ws.Cells["A4"].Value = "Date";
            ws.Cells["B4"].Value = String.Format("From {0:dd MMMM yyyy} to {1:dd MMMM yyyy}", DateTime.Parse(fromExpenses), DateTime.Parse(toExpenses));

            ws.Cells["E4"].Value = "Date";
            ws.Cells["F4"].Value = String.Format("From {0:dd MMMM yyyy} to {1:dd MMMM yyyy}", DateTime.Parse(fromIncomes), DateTime.Parse(toIncomes));


            ws.Cells["A6"].Value = "Type";
            ws.Cells["B6"].Value = "Amount";

            int rowStart = 7;
            foreach (ExpenseAmount expense in viewModel.Expenses)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = expense.Type;
                ws.Cells[string.Format("B{0}", rowStart)].Value = expense.Amount.ToString();
                rowStart++;
            }

            ws.Cells["E6"].Value = "Type";
            ws.Cells["F6"].Value = "Amount";

            rowStart = 7;
            foreach (IncomeAmount income in viewModel.Incomes)
            {
                ws.Cells[string.Format("E{0}", rowStart)].Value = income.Type;
                ws.Cells[string.Format("F{0}", rowStart)].Value = income.Amount.ToString();
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.Body.WriteAsync(pck.GetAsByteArray());
        }


        [Authorize]
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
