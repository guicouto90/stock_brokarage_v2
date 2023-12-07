using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class StockBrokarageDbContext : DbContext
    {
        public StockBrokarageDbContext(DbContextOptions<StockBrokarageDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockBrokarageDbContext).Assembly);
        }
    }
}
