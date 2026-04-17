using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate).IsRequired();

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");

            builder.Property(o => o.DiscountAmount).HasColumnType("decimal(18,2)");

            builder.Property(o => o.TaxAmount).HasColumnType("decimal(18,2)");

            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
