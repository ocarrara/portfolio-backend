using Microsoft.EntityFrameworkCore;
using PortfolioApp.Models;

namespace PortfolioApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<PortfolioItem> PortfolioItems { get; set; } = null!;

    }
}
