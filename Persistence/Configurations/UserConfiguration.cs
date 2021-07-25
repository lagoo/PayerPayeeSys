using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .ToTable("Users");

            builder
                .HasKey(e => e.Id);

            builder
                .HasIndex(e => e.Email);

            builder
                .HasIndex(e => e.Document);

            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder
                .Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(5000)");

            builder
                .Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder
                .Property(e => e.Document)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
