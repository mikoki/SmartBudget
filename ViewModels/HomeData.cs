using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.ViewModels
{
    public class HomeData
    {
        public IEnumerable<ExpenseAmount> Expenses { get; set; }
        public IEnumerable<IncomeAmount> Incomes { get; set; }
        public User User { get; set; }
    }
}
