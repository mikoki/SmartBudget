using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.ViewModels
{
    public class ExpenseIndexData
    {
        public ExpenseType ExpenseType { get; set; }
        public IEnumerable<ExpenseType> ExpensesType { get; set; }
        public Expense Expense { get; set; }
        public IQueryable<Expense> Expenses { get; set; }
    }
}
