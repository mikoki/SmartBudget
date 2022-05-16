using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.ViewModels
{
    public class ReminderCategories
    {
        public IEnumerable<Reminder> CurrentReminders { get; set; }
        public IEnumerable<Reminder> UpcomingReminders { get; set; }
        public IEnumerable<Reminder> PastReminders { get; set; }
    }
}
