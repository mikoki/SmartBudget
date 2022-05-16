using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartBudget.Migrations
{
    public partial class createDbWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsLogged = table.Column<bool>(type: "boolean", nullable: false),
                    isBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncomeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfReminding = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Savings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    AmountGoal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Savings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Savings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ExpenseTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseTypes_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IncomeTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_IncomeTypes_IncomeTypeId",
                        column: x => x.IncomeTypeId,
                        principalTable: "IncomeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavingLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SavingId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingLogs_Savings_SavingId",
                        column: x => x.SavingId,
                        principalTable: "Savings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "Electricity", null },
                    { 2, "Water", null },
                    { 3, "Cell phone", null }
                });

            migrationBuilder.InsertData(
                table: "IncomeTypes",
                columns: new[] { "Id", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "Salary", null },
                    { 2, "Rent", null },
                    { 3, "Investment", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "IsLogged", "LastName", "Password", "RoleId", "Username", "isBlocked" },
                values: new object[,]
                {
                    { 1, new DateTime(1982, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "g.petrov@abv.bg", "Georgi", false, "Petkov", "th/k3h5jtdWPEF0kFbvQVFe5PaeC3RUXGXeUjBriM+4HIcIN", 1, "gpetrovADM", false },
                    { 2, new DateTime(1994, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "k.jekova@abv.bg", "Krasimira", false, "Jekova", "ncyM9SmsO32kMdh6sAt+IGtw5UBnW3GfVCchwGc7drT/dsCY", 1, "kjekovaADM", false },
                    { 3, new DateTime(1978, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "a.dimitrov@abv.bg", "Anton", false, "Dimitrov", "2IlfBq/nKH95mXK6pliXs0YkVU/Mqfmo2QOFepjLgoZuhFTu", 2, "andim558", false },
                    { 4, new DateTime(1990, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "v.hristova@abv.bg", "Valq", false, "Hristova", "ytCqnO71zyzx22/SYMFDoX/oYSYBY/hG+dde9rdJbwkT7aUe", 2, "valhristova", false },
                    { 5, new DateTime(1973, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "a.vladimirov@abv.bg", "Asen", false, "Vladimirov", "mwpYsRolUMs29HXNBn/IPLZG6KWjfGuNUIfNkcQByDhvMTuH", 2, "a.vladimirov11", false }
                });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Type", "UserId" },
                values: new object[,]
                {
                    { 4, "Loan", 2 },
                    { 6, "Gasoline", 3 },
                    { 7, "Rent", 3 },
                    { 5, "Auto insurance", 2 }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "CreatedAt", "ExpenseTypeId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 40m, new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "September 2020 - bills", 1 },
                    { 2, 50m, new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "October 2021 - bills", 1 },
                    { 3, 30m, new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "October 2021 - bills", 1 },
                    { 4, 25m, new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "September 2021 - bills", 1 },
                    { 5, 28m, new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "October 2021 - bills", 1 }
                });

            migrationBuilder.InsertData(
                table: "IncomeTypes",
                columns: new[] { "Id", "Type", "UserId" },
                values: new object[,]
                {
                    { 5, "Lottery", 2 },
                    { 6, "Shares", 3 },
                    { 4, "Sale", 2 },
                    { 7, "Dividends", 3 }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "Amount", "CreatedAt", "IncomeTypeId", "Title", "UserId" },
                values: new object[,]
                {
                    { 4, 45m, new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "September 2021", 1 },
                    { 3, 100m, new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "October 2021 - apartment", 1 },
                    { 2, 820m, new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Job - October 2021", 1 },
                    { 1, 800m, new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Job - September 2021", 1 },
                    { 5, 80m, new DateTime(2021, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "October 2021", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reminders",
                columns: new[] { "Id", "CreatedAt", "DateOfReminding", "Description", "IsDisplayed", "Title", "UserId" },
                values: new object[,]
                {
                    { 5, new DateTime(2022, 5, 7, 0, 51, 40, 933, DateTimeKind.Local).AddTicks(6608), new DateTime(2022, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "", false, "Insurance", 2 },
                    { 4, new DateTime(2022, 5, 7, 0, 51, 40, 933, DateTimeKind.Local).AddTicks(6572), new DateTime(2022, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "TV", false, "Bills - January", 1 },
                    { 3, new DateTime(2022, 5, 7, 0, 51, 40, 933, DateTimeKind.Local).AddTicks(6525), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "", false, "Third-party liability insurance", 1 },
                    { 2, new DateTime(2022, 5, 7, 0, 51, 40, 933, DateTimeKind.Local).AddTicks(6304), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "200 in the bank", true, "Deposit", 1 },
                    { 1, new DateTime(2022, 5, 7, 0, 51, 40, 933, DateTimeKind.Local).AddTicks(1408), new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water and Electricity", false, "Bills - October", 1 }
                });

            migrationBuilder.InsertData(
                table: "Savings",
                columns: new[] { "Id", "AmountGoal", "CreatedAt", "CurrentAmount", "IsCompleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 800m, new DateTime(2022, 5, 7, 0, 51, 40, 920, DateTimeKind.Local).AddTicks(489), 15m, false, "Car", 1 },
                    { 3, 100m, new DateTime(2022, 5, 7, 0, 51, 40, 930, DateTimeKind.Local).AddTicks(8720), 80m, false, "Charity", 2 },
                    { 4, 45m, new DateTime(2022, 5, 7, 0, 51, 40, 930, DateTimeKind.Local).AddTicks(8741), 10m, false, "Dress", 3 },
                    { 2, 820m, new DateTime(2022, 5, 7, 0, 51, 40, 930, DateTimeKind.Local).AddTicks(8468), 800m, false, "Phone", 1 },
                    { 5, 80m, new DateTime(2022, 5, 7, 0, 51, 40, 930, DateTimeKind.Local).AddTicks(8755), 20m, false, "Rent", 3 }
                });

            migrationBuilder.InsertData(
                table: "SavingLogs",
                columns: new[] { "Id", "Amount", "SavingId", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 40m, 1, 0, new DateTime(2022, 5, 7, 0, 51, 40, 932, DateTimeKind.Local).AddTicks(2042) },
                    { 2, 20m, 1, 1, new DateTime(2022, 5, 7, 0, 51, 40, 932, DateTimeKind.Local).AddTicks(3778) },
                    { 3, 60m, 1, 0, new DateTime(2022, 5, 7, 0, 51, 40, 932, DateTimeKind.Local).AddTicks(3833) },
                    { 4, 15m, 2, 0, new DateTime(2022, 5, 7, 0, 51, 40, 932, DateTimeKind.Local).AddTicks(3846) },
                    { 5, 20m, 2, 0, new DateTime(2022, 5, 7, 0, 51, 40, 932, DateTimeKind.Local).AddTicks(3856) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseTypeId",
                table: "Expenses",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_UserId",
                table: "ExpenseTypes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IncomeTypeId",
                table: "Incomes",
                column: "IncomeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTypes_UserId",
                table: "IncomeTypes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingLogs_SavingId",
                table: "SavingLogs",
                column: "SavingId");

            migrationBuilder.CreateIndex(
                name: "IX_Savings_UserId",
                table: "Savings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "SavingLogs");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "IncomeTypes");

            migrationBuilder.DropTable(
                name: "Savings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
