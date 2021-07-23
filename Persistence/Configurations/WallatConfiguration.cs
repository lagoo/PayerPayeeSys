using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WallatConfiguration : IEntityTypeConfiguration<Wallat>
    {
        public void Configure(EntityTypeBuilder<Wallat> builder)
        {
            builder
               .ToTable("Wallats");

            builder
                .HasKey(e => e.Id);

            builder
                .HasOne(e => e.User)
                .WithOne(e => e.Wallat)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
