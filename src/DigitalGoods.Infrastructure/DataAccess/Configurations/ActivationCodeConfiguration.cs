using DigitalGoods.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalGoods.Infrastructure.DataAccess.Configurations
{
    public class ActivationCodeConfiguration : IEntityTypeConfiguration<ActivationCode>
    {
        public void Configure(EntityTypeBuilder<ActivationCode> builder)
        {
            builder.HasOne(c => c.Offer)
                .WithMany(o => o.ActivationCodes)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
