using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<PortfolioItem> PortfolioItems { get; set; } = null!;

    }
}
