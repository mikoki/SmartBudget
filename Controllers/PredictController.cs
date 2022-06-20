using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using SmartBudget.Data;
using SmartBudget.Models;
using SmartBudget.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartBudget.Controllers
{
    public class PredictController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PredictController(ApplicationDbContext db)
        {
            _db = db;
        }

        private User GetUserByUsername()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            return user;
        }
        private static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }

        [Authorize]
        public IActionResult Index()
        {
            Dictionary<int, decimal> algorithmData = new Dictionary<int, decimal>();
            Dictionary<int, string> chartData = new Dictionary<int, string>();
            List<DataPoint> dataPoints = new List<DataPoint>();

            int lastMonth = 0;
            int nextMonth = 0;
            int lastYear = 0;
            int nextYear = 0;

            User user = GetUserByUsername();
            using (NpgsqlConnection connection = new NpgsqlConnection(_db.Database.GetConnectionString()))
            {
                string getBalances = @"
                                        with expenses as (
                                            select
                                                extract (year from e.""CreatedAt"") as ""year"",
                                                extract (month from e.""CreatedAt"") as ""month"",
                                                sum(e.""Amount"") as ""amount""
                                            from ""Expenses"" e where e.""UserId"" = 1
                                            group by ""year"", ""month""
                                        ),
                                        incomes as (
                                            select
                                                extract (year from i.""CreatedAt"") as ""year"",
                                                extract (month from i.""CreatedAt"") as ""month"",
                                                sum(i.""Amount"") as ""amount"" 
                                            from ""Incomes"" i where i.""UserId"" = 1
                                            group by ""year"", ""month""
                                        )
                                        select 
                                            coalesce(e.""year"", i.""year"")::int as ""year"",
                                            coalesce(e.""month"", i.""month"")::int as ""month"",    
                                            coalesce(i.amount, 0) - coalesce(e.amount, 0) as balance
                                        from
	                                        expenses as e
                                        full join
	                                        incomes i on i.""year"" = e.""year"" and i.""month"" = e.""month""
                                        order by
                                            coalesce(e.""year"", i.""year""),
	                                        coalesce(e.""month"", i.""month"")
                                    ";
                NpgsqlCommand sqlCommand = new NpgsqlCommand(getBalances, connection);
                try
                {

                    connection.Open();
                    NpgsqlDataReader dataReader = sqlCommand.ExecuteReader();
                    int count = 1;
                    while (dataReader.Read())
                    {
                        var values = new object[dataReader.FieldCount];
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            values[i] = dataReader[i];
                        }

                        algorithmData.Add(count, ConvertFromDBVal<decimal>(values[2]));
                        chartData.Add(count, ConvertFromDBVal<int>(values[0]) + "-" + ConvertFromDBVal<int>(values[1]));
                        lastYear = ConvertFromDBVal<int>(values[0]);
                        lastMonth = ConvertFromDBVal<int>(values[1]);
                        count++;
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }

            double meanX = 0.0;
            decimal meanY = 0.0m;
            double sumOffsetXMulOffsetY = 0.0;
            double xOffsetSquaredSum = 0.0;

           foreach (var data in algorithmData)
            {
                meanX = meanX + data.Key;
                meanY = meanY + data.Value;
            }

            meanX = meanX / algorithmData.Count();
            meanY = meanY / algorithmData.Count();

            foreach (var data in algorithmData)
            {
                sumOffsetXMulOffsetY = sumOffsetXMulOffsetY + (data.Key - meanX) * (double)(data.Value - meanY);
                xOffsetSquaredSum = xOffsetSquaredSum + Math.Pow(data.Key - meanX, 2);
                dataPoints.Add(new DataPoint(chartData.GetValueOrDefault(data.Key), data.Value));
            }

            double b1 = sumOffsetXMulOffsetY / xOffsetSquaredSum;
            double b0 = (double)meanY - (meanX * b1);
            Console.WriteLine(b1);
            Console.WriteLine(b0);
            double predictedBalance = b0 + b1 * (algorithmData.Count() + 1);

            if(lastMonth == 12)
            {
                nextMonth = 1;
                nextYear = lastYear + 1;
            }
            else
            {
                nextMonth = lastMonth + 1;
                nextYear = lastYear;
            }

            dataPoints.Add(new DataPoint(nextYear + "-" + nextMonth, (decimal)predictedBalance));
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}
