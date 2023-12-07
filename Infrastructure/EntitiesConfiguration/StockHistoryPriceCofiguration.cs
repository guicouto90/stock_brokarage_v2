using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class StockHistoryPriceCofiguration
        : IEntityTypeConfiguration<StockHistoryPrice>
    {
        public void Configure(EntityTypeBuilder<StockHistoryPrice> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ActualPrice).HasPrecision(10, 2).IsRequired();

            builder.Property(shp => shp.Id).UseIdentityColumn();
        }
    }
}
