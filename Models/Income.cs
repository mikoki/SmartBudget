using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class Income
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)"),]
        [Range(0, double.MaxValue, ErrorMessage = "Please provide positive number.")]
        public decimal Amount { get; set; }

        //----------------------- Many incomes relationship-----------------------
        public int UserId { get; set; }
        public virtual User User { get; set; } // one user

        public int IncomeTypeId { get; set; }
        public virtual IncomeType IncomeType { get; set; } // one income type
        //----------------------- Many incomes relationship-----------------------
    
        [Required]
        [Display(Name = "Date of income")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

    }
}
