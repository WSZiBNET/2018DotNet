using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class Employee : User
    {
        public int EmployeeID { get; set; }
        public string PositionID { get; set; }
        public string DepartmentID { get; set; }
        public double OpinionRate { get; set; }
        [Required]
        public decimal Salary { get; set; }
     
    }
}
