using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.ViewModels
{
    public class SavingSavingLogs
    {
        public Saving Saving { get; set; }
        public SavingLog SavingLog { get; set; }
        public IEnumerable<SavingLog> SavingLogs{ get; set; }
    }
}
