using Microsoft.EntityFrameworkCore;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Author> Author { get; set; }
    }
}
