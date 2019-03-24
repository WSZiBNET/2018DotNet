using Microsoft.EntityFrameworkCore;
using School3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School3.Data
{
    public class PrzedmiotContext : DbContext
    {
        public PrzedmiotContext(DbContextOptions<PrzedmiotContext> options) : base(options)
        {

        }

        public DbSet<Przedmiot> Przedmiot { get; set; }
    }
}
