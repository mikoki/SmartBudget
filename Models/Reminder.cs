using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class Reminder
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3,ErrorMessage = "Title should be at least three symbols")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfReminding { get; set; }
        
        public bool IsDisplayed { get; set; }

        //----------------------- Many reminders relationship-----------------------
        public int UserId { get; set; }
        public virtual User User { get; set; }
        //----------------------- Many reminders relationship-----------------------
    }
}
