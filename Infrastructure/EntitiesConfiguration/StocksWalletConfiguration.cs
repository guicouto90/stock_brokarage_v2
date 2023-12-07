using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.EntitiesConfiguration
{
    public class StocksWalletConfiguration : IEntityTypeConfiguration<StocksWallet>
    {
        public void Configure(EntityTypeBuilder<StocksWallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(sw => sw.StockQuantity).IsRequired();
            builder.Property(sw => sw.AveragePrice).IsRequired();
            builder.Property(sw => sw.CurrentInvestedStock).IsRequired();
            builder.Property(sw => sw.TotalInvestedStock).IsRequired();

            builder.HasOne(sw => sw.Stock)
                .WithMany()
                .HasForeignKey(sw => sw.StockId)
                .IsRequired();
        }
    }
}
