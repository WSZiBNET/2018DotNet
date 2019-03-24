using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPWlasciwosci.Controllers
{
    public class EmployeeController : Controller
    {
        [Authorize(Policy = "WymagajManagera")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return View();
        }
    }
}