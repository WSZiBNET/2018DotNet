using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class SalaryIncrease
    {
        public int EmployeeID { get; set; }
        public int IncreaseID { get; set; }
        public decimal IncreaseValue { get; set; }
    }
}
