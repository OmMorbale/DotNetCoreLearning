using Microsoft.EntityFrameworkCore;

namespace LearningDotNetCoreAPI.Data
{
    public class AppDbContext: DbContext
    {
        // This constructor lets DI hand you your options (connection string etc.)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }

        // AppDbContext.cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Amount)
                .HasPrecision(18, 2);
        }
    }
}
