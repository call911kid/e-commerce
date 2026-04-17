using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);

            builder.Property(c => c.Email).IsRequired().HasMaxLength(255);

            builder.Property(c => c.PasswordHash).IsRequired().HasMaxLength(255);

            builder.Property(c => c.Address).HasMaxLength(500);
        }
    }
}
