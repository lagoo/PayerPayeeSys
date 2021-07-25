using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TransactionNotificationConfiguration : IEntityTypeConfiguration<TransactionNotification>
    {
        public void Configure(EntityTypeBuilder<TransactionNotification> builder)
        {
            builder
              .ToTable("TransactionNotifications");

            builder
                .HasKey(e => e.Id);
            
            builder
                .HasOne(e => e.Transaction)
                .WithMany()
                .HasForeignKey(e => e.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
