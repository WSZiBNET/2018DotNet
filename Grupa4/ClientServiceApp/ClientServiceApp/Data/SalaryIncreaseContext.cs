using ClientServiceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Data
{
    public class SalaryIncreaseContext : DbContext
    {
        public SalaryIncreaseContext(DbContextOptions<SalaryIncreaseContext> options) : base(options)
        {

        }

        public DbSet<SalaryIncrease> SalaryChanges { get; set; }
    }
}
