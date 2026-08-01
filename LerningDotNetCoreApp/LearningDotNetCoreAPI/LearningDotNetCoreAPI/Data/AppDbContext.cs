using Microsoft.EntityFrameworkCore;

namespace LearningDotNetCoreAPI.Data
{
    public class AppDbContext: DbContext
    {
        // This constructor lets DI hand you your options (connection string etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
