using Microsoft.EntityFrameworkCore;
using School3.Models;

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
