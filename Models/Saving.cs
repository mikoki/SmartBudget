using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class Saving
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)"),]
        [Range(0, double.MaxValue, ErrorMessage = "Please provide positive number.")]
        public decimal AmountGoal { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)"),]
        [Range(0, double.MaxValue, ErrorMessage = "Please provide positive number.")]
        public decimal CurrentAmount { get; set; }

        //------------------------ Many savings relationship------------------------
        public int UserId { get; set; } // one user
        public virtual User User { get; set; }

        //------------------------ Many savings relationship------------------------
        
        //------------------------ One saving relationship------------------------
        public virtual ICollection<SavingLog> SavingLog { get; set; } //many saving logs

        //------------------------ One saving relationship------------------------

        [Required]
        [Display(Name = "Date of creation")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;
    }
}
