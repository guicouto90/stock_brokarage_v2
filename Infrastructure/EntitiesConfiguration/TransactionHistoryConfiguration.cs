using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.HasKey(th => th.Id);

            builder.Property(th => th.TransactionValue).IsRequired();
            builder.Property(th => th.TypeTransaction).HasConversion<string>().IsRequired();
            builder.Property(th => th.StockQuantity);
            builder.Property(th => th.StockPrice);
            builder.Property(th => th.StockCode).HasMaxLength(5);
            builder.Property(th => th.Date).IsRequired();
        }
    }
}
