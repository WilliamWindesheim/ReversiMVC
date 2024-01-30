using Microsoft.EntityFrameworkCore;
using ReversiMVCProper.Models;

namespace ReversiMVCProper.Data
{
    public class ReversiDbContext : DbContext
    {
        public ReversiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Speler> Spelers { get; set; }

        public DbSet<ReversiMVCProper.Models.Spel> Spel { get; set; }
    }
}
