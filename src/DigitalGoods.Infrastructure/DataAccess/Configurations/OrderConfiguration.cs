using DigitalGoods.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalGoods.Infrastructure.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(s => s.Buyer)
                .WithMany(b => b.Purchases)
                .HasForeignKey(s => s.BuyerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull); 

            builder.HasOne(s => s.Offer)
                .WithMany(o => o.Sales)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
