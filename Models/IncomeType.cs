using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class IncomeType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        //----------------------- Many income type relationship-----------------------
        public int? UserId { get; set; } //one user, ? means that some imcome types don't have user
        public virtual User User { get; set; }
        //----------------------- Many income type relationship-----------------------
    }
}
