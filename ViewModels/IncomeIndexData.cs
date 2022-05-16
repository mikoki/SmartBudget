using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.ViewModels
{
    public class IncomeIndexData
    {
        public IncomeType IncomeType { get; set; }
        public IEnumerable<IncomeType> IncomesType { get; set; }
        public Income Income { get; set; }
        public IQueryable<Income> Incomes { get; set; }
    }
}
