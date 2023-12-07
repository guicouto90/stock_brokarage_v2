using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.TotalInvested).IsRequired();
            builder.Property(w => w.CurrentBalance).IsRequired();

            builder.HasMany(s => s.StocksWallet)
                .WithOne()
                .HasForeignKey(sw => sw.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
