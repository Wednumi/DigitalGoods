using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalGoods.Infrastructure.DataAccess.Configurations
{
    public class Media : IEntityTypeConfiguration<Core.Entities.Media>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Media> builder)
        {
            builder.HasOne(m => m.Offer)
                .WithMany(o => o.Medias)
                .HasForeignKey(m => m.OfferId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
