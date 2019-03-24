using Microsoft.EntityFrameworkCore;
using School3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School3.Data
{
    public class NauczycielContext : DbContext
    {
        public NauczycielContext(DbContextOptions<NauczycielContext> options) : base(options)
        {

        }

        public DbSet<Nauczyciel> Nauczyciel { get; set; }
    }
}
