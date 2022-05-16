using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class Expense
    {   [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)"),]
        [Range(0, double.MaxValue, ErrorMessage = "Please provide positive number.")]
        public decimal Amount { get; set; }

        //----------------------- Many expenses relationship-----------------------
        public int UserId { get; set; } // one user
        public virtual User User { get; set; }

        [Required(ErrorMessage ="Please provide valid type")]
        public int ExpenseTypeId { get; set; } // one expense type
        [Display(Name = "Type")]
        public virtual ExpenseType ExpenseType { get; set; }
        //----------------------- Many expenses relationship-----------------------

        [Required]
        [Display(Name = "Date of payment")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
    }
}
