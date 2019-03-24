using Microsoft.EntityFrameworkCore;

namespace School3.Data
{
    public class UczenContext : DbContext
    {
        public UczenContext(DbContextOptions<UczenContext> options) : base(options)
        {

        }

        public DbSet<Models.Uczen> Uczen { get; set; }
    }
}
