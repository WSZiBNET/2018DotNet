using Microsoft.EntityFrameworkCore;
using School3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School3.Data
{
    public class NauczucielContext : DbContext
    {
        public NauczucielContext(DbContextOptions<NauczucielContext> options) : base(options)
        {

        }

        public DbSet<Nauczuciel> Nauczyciel { get; set; }
    }
}
