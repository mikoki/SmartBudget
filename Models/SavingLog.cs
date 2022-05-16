using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class SavingLog
    {
        public enum ExchangeType {Save, Withdraw}

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)"),]
        [Range(0, double.MaxValue, ErrorMessage = "Please provide positive number.")]
        public decimal Amount { get; set; }

        [Required]
        public ExchangeType Type { get; set; }
        //----------------------- Many expenses relationship-----------------------
        public int SavingId { get; set; } // one user
        public virtual Saving Saving { get; set; }

        //----------------------- Many expenses relationship-----------------------
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }


    }
}
