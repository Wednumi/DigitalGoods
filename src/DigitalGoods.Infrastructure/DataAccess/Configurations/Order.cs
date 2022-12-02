using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalGoods.Infrastructure.DataAccess.Configurations
{
    public class Order : IEntityTypeConfiguration<Core.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Order> builder)
        {
            builder.HasOne(s => s.Buyer)
                .WithMany(b => b.Purchases)
                .HasForeignKey(s => s.BuyerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull); 

            builder.HasOne(s => s.Offer)
                .WithMany(o => o.Sales)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
