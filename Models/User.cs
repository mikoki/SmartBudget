using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public class User
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("username")]
        [MinLength(6, ErrorMessage ="Username should be at least 6 symbols")]
        [MaxLength(20, ErrorMessage = "Username should be at most 20 symbols")]
        public string Username { get; set; }

        [Required]
        //[RegularExpression(@"@+", ErrorMessage = "Password should contain at least one special symbol")]
        [DisplayName("password")]
        [MinLength(8, ErrorMessage ="Password should be at least 8 symbols")]
        public string Password { get; set; }


        [Required]
        [DisplayName("email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("first name")]
        [MinLength(1, ErrorMessage = "First name should be at least 6 symbols")]
        [MaxLength(30, ErrorMessage = "First name should be at most 30 symbols")]
        public string FirstName { get; set; }
       
        [Required]
        [DisplayName("last name")]
        [MinLength(1, ErrorMessage = "Last name should be at least 6 symbols")]
        [MaxLength(30, ErrorMessage = "First name should be at most 30 symbols")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [ValidateNever]
        public bool IsLogged { get; set; }

        [Required]
        [ValidateNever]
        public bool isBlocked { get; set; }
        //----------------------- Many users relationship-----------------------
        [Required]
        [ValidateNever] 
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } //one role

        //----------------------- Many users relationship-----------------------

        //------------------------ One user relationship------------------------
        public virtual ICollection<Expense> Expense { get; set; } //many expenses
        public virtual ICollection<ExpenseType> ExpenseType { get; set; } //many expense types

        public virtual ICollection<Income> Income { get; set; } //many incomes
        public virtual ICollection<IncomeType> IncomeType { get; set; } //many expense types

        public virtual ICollection<Saving> Saving { get; set; } //many savings
        public virtual ICollection<Reminder> Reminder { get; set; } //many reminders
        //------------------------ One user relationship------------------------
    }
 
}
