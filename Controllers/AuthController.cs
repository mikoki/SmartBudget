using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Controllers
{
    public class AuthController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        public String Index()
        {
            return "Hello world";
        }
    }
}
