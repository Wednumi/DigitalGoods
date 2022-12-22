using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalGoods.Infrastructure.DataAccess.Configurations
{
    public class ActivationCode : IEntityTypeConfiguration<Core.Entities.ActivationCode>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ActivationCode> builder)
        {
            builder.HasOne(c => c.Offer)
                .WithMany(o => o.ActivationCodes)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
