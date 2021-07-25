using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
               .ToTable("Transactions");

            builder
                .HasKey(e => e.Id);            

            builder
                .HasOne(e => e.Payer)
                .WithMany()
                .HasForeignKey(e => e.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Payee)
                .WithMany()
                .HasForeignKey(e => e.PayeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
