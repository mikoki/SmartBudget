using Microsoft.EntityFrameworkCore;
using SmartBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                    new Role { Id = 1, RoleName = "Admin" },
                    new Role { Id = 2, RoleName = "User" }
                );

            builder.Entity<User>().HasData(
                    new User { Id = 1, Username = "gpetrovADM", Password = "th/k3h5jtdWPEF0kFbvQVFe5PaeC3RUXGXeUjBriM+4HIcIN", Email = "g.petrov@abv.bg", FirstName = "Georgi", LastName = "Petkov", BirthDate = DateTime.Parse("19/09/1982"), IsLogged = false, isBlocked = false, RoleId = 1 },
                    new User { Id = 2, Username = "kjekovaADM", Password = "ncyM9SmsO32kMdh6sAt+IGtw5UBnW3GfVCchwGc7drT/dsCY", Email = "k.jekova@abv.bg", FirstName = "Krasimira", LastName = "Jekova", BirthDate = DateTime.Parse("23/01/1994"), IsLogged = false, isBlocked = false, RoleId = 1 },
                    new User { Id = 3, Username = "andim558", Password = "2IlfBq/nKH95mXK6pliXs0YkVU/Mqfmo2QOFepjLgoZuhFTu", Email = "a.dimitrov@abv.bg", FirstName = "Anton", LastName = "Dimitrov", BirthDate = DateTime.Parse("15/02/1978"), IsLogged = false, isBlocked = false, RoleId = 2 },
                    new User { Id = 4, Username = "valhristova", Password = "ytCqnO71zyzx22/SYMFDoX/oYSYBY/hG+dde9rdJbwkT7aUe", Email = "v.hristova@abv.bg", FirstName = "Valq", LastName = "Hristova", BirthDate = DateTime.Parse("07/09/1990"), IsLogged = false, isBlocked = false, RoleId = 2 },
                    new User { Id = 5, Username = "a.vladimirov11", Password = "mwpYsRolUMs29HXNBn/IPLZG6KWjfGuNUIfNkcQByDhvMTuH", Email = "a.vladimirov@abv.bg", FirstName = "Asen", LastName = "Vladimirov", BirthDate = DateTime.Parse("03/04/1973"), IsLogged = false, isBlocked = false, RoleId = 2 }
                    );

            builder.Entity<ExpenseType>().HasData(
                    new ExpenseType { Id = 1, Type = "Electricity", UserId = null },
                    new ExpenseType { Id = 2, Type = "Water", UserId = null },
                    new ExpenseType { Id = 3, Type = "Cell phone", UserId = null },
                    new ExpenseType { Id = 4, Type = "Loan", UserId = 2 },
                    new ExpenseType { Id = 5, Type = "Auto insurance", UserId = 2 },
                    new ExpenseType { Id = 6, Type = "Gasoline", UserId = 3 },
                    new ExpenseType { Id = 7, Type = "Rent", UserId = 3 }
                );

            builder.Entity<Expense>().HasData(
                new Expense { Id = 1, Title = "September 2020 - bills", Amount = Convert.ToDecimal(40.00), UserId = 1, ExpenseTypeId = 1, CreatedAt = DateTime.Parse("3/10/2021") },
                new Expense { Id = 2, Title = "October 2021 - bills", Amount = Convert.ToDecimal(50.00), UserId = 1, ExpenseTypeId = 1, CreatedAt = DateTime.Parse("3/11/2021") },
                new Expense { Id = 3, Title = "October 2021 - bills", Amount = Convert.ToDecimal(30.00), UserId = 1, ExpenseTypeId = 2, CreatedAt = DateTime.Parse("3/11/2021") },
                new Expense { Id = 4, Title = "September 2021 - bills", Amount = Convert.ToDecimal(25.00), UserId = 1, ExpenseTypeId = 3, CreatedAt = DateTime.Parse("3/10/2021") },
                new Expense { Id = 5, Title = "October 2021 - bills", Amount = Convert.ToDecimal(28.00), UserId = 1, ExpenseTypeId = 3, CreatedAt = DateTime.Parse("3/11/2021") }
            );


            builder.Entity<IncomeType>().HasData(
                new IncomeType { Id = 1, Type = "Salary", UserId = null },
                new IncomeType { Id = 2, Type = "Rent", UserId = null },
                new IncomeType { Id = 3, Type = "Investment", UserId = null },
                new IncomeType { Id = 4, Type = "Sale", UserId = 2 },
                new IncomeType { Id = 5, Type = "Lottery", UserId = 2 },
                new IncomeType { Id = 6, Type = "Shares", UserId = 3 },
                new IncomeType { Id = 7, Type = "Dividends", UserId = 3 }
            );

            builder.Entity<Income>().HasData(
                new Income { Id = 1, Title = "Job - September 2021", Amount = Convert.ToDecimal(800.00), UserId = 1, IncomeTypeId = 1, CreatedAt = DateTime.Parse("3/10/2021") },
                new Income { Id = 2, Title = "Job - October 2021", Amount = Convert.ToDecimal(820.00), UserId = 1, IncomeTypeId = 1, CreatedAt = DateTime.Parse("3/11/2021") },
                new Income { Id = 3, Title = "October 2021 - apartment", Amount = Convert.ToDecimal(100.00), UserId = 1, IncomeTypeId = 2, CreatedAt = DateTime.Parse("3/11/2021") },
                new Income { Id = 4, Title = "September 2021", Amount = Convert.ToDecimal(45.00), UserId = 1, IncomeTypeId = 3, CreatedAt = DateTime.Parse("3/10/2021") },
                new Income { Id = 5, Title = "October 2021", Amount = Convert.ToDecimal(80.00), UserId = 1, IncomeTypeId = 3, CreatedAt = DateTime.Parse("3/11/2021") }
            );

            builder.Entity<Saving>().HasData(
                new Saving { Id = 1, Title = "Car", AmountGoal = Convert.ToDecimal(800.00), CurrentAmount = Convert.ToDecimal(15.00), UserId = 1, CreatedAt = DateTime.Now},
                new Saving { Id = 2, Title = "Phone", AmountGoal = Convert.ToDecimal(820.00), CurrentAmount = Convert.ToDecimal(800.00), UserId = 1, CreatedAt = DateTime.Now},
                new Saving { Id = 3, Title = "Charity", AmountGoal = Convert.ToDecimal(100.00), CurrentAmount = Convert.ToDecimal(80.00), UserId = 2, CreatedAt = DateTime.Now},
                new Saving { Id = 4, Title = "Dress", AmountGoal = Convert.ToDecimal(45.00), CurrentAmount = Convert.ToDecimal(10.00), UserId = 3, CreatedAt = DateTime.Now},
                new Saving { Id = 5, Title = "Rent", AmountGoal = Convert.ToDecimal(80.00), CurrentAmount = Convert.ToDecimal(20.00), UserId = 3, CreatedAt = DateTime.Now}
            );

            builder.Entity<SavingLog>().HasData(
                new SavingLog { Id = 1, Amount = Convert.ToDecimal(40.00), Type = SavingLog.ExchangeType.Save, SavingId = 1, UpdatedAt = DateTime.Now},
                new SavingLog { Id = 2, Amount = Convert.ToDecimal(20.00), Type = SavingLog.ExchangeType.Withdraw, SavingId = 1, UpdatedAt = DateTime.Now },
                new SavingLog { Id = 3, Amount = Convert.ToDecimal(60.00), Type = SavingLog.ExchangeType.Save, SavingId = 1, UpdatedAt = DateTime.Now },
                new SavingLog { Id = 4, Amount = Convert.ToDecimal(15.00), Type = SavingLog.ExchangeType.Save, SavingId = 2, UpdatedAt = DateTime.Now },
                new SavingLog { Id = 5, Amount = Convert.ToDecimal(20.00), Type = SavingLog.ExchangeType.Save, SavingId = 2, UpdatedAt = DateTime.Now }
            );

            builder.Entity<Reminder>().HasData(
                new Reminder { Id = 1, Title = "Bills - October", Description = "Water and Electricity", CreatedAt = DateTime.Now, DateOfReminding = DateTime.Parse("3/1/2022"), IsDisplayed = false, UserId = 1},
                new Reminder { Id = 2, Title = "Deposit", Description = "200 in the bank", CreatedAt = DateTime.Now, DateOfReminding = DateTime.Parse("1/1/2022"), IsDisplayed = true, UserId = 1 },
                new Reminder { Id = 3, Title = "Third-party liability insurance", Description = "", CreatedAt = DateTime.Now, DateOfReminding = DateTime.Parse("15/1/2022"), IsDisplayed = false, UserId = 1},
                new Reminder { Id = 4, Title = "Bills - January", Description = "TV", CreatedAt = DateTime.Now, DateOfReminding = DateTime.Parse("6/1/2022"), IsDisplayed = false, UserId = 1 },
                new Reminder { Id = 5, Title = "Insurance", Description = "", CreatedAt = DateTime.Now, DateOfReminding = DateTime.Parse("8/1/2022"), IsDisplayed = false, UserId = 2 }
            );

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }


        public DbSet<Saving> Savings { get; set; }
        public DbSet<SavingLog> SavingLogs { get; set; }
    
        public DbSet<Reminder> Reminders { get; set; }
    }


}
