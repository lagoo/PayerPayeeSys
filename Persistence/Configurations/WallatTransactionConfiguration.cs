using Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WallatTransactionConfiguration : IEntityTypeConfiguration<WallatTransaction>
    {
        public void Configure(EntityTypeBuilder<WallatTransaction> builder)
        {
            builder
               .ToTable("WallatTransactions");

            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Amount)
                .HasColumnType($"decimal(18,{SystemConst.DECIMAL_ROUND})");

            builder
                .HasOne(e => e.Wallat)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.WallatId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
