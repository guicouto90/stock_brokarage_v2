using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Code).HasMaxLength(5).IsRequired();
            builder.Property(t => t.Price).HasPrecision(10, 2).IsRequired();

            builder.HasMany(s => s.History)
                .WithOne()
                .HasForeignKey(s => s.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
