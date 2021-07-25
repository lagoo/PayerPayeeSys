using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder
               .ToTable("Wallets");

            builder
                .HasKey(e => e.Id);

            builder
                .HasOne(e => e.User)
                .WithOne(e => e.Wallet)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
