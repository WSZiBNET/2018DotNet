using Microsoft.EntityFrameworkCore;
using School3.Models;

namespace School3.Data
{
    public class UczenContext : DbContext
    {
        public UczenContext(DbContextOptions<UczenContext> options) : base(options)
        {

        }

        public DbSet<Uczen> Uczen { get; set; }
    }
}
