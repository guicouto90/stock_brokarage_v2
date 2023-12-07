using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AccountNumber).IsRequired();
            builder.Property(a => a.Balance).IsRequired();
            builder.Property(a => a.Password).IsRequired();

            builder
              .HasOne(c => c.Customer)
              .WithOne(a => a.Account)
              .HasForeignKey<Customer>()
              .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(w => w.Wallet)
                .WithOne()
                .HasForeignKey<Wallet>(w => w.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.TransactionHistories)
                .WithOne()
                .HasForeignKey(s => s.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
