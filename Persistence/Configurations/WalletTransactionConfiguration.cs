using Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder
               .ToTable("WalletTransactions");

            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Amount)
                .HasColumnType($"decimal(18,{SystemConst.DECIMAL_ROUND})");

            builder
                .HasOne(e => e.Wallet)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.WalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
