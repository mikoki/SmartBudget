using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class ExpenseType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Type")]
        [Required]
        [MinLength(1,ErrorMessage = "Type should contain at least one symbol")]
        public string Type { get; set; }

        //----------------------- Many expence type relationship-----------------------
        public int? UserId { get; set; }//one user, ? means that some expense types don't have user
        public virtual User User { get; set; }
        //----------------------- Many expence type relationship-----------------------
    }
}
