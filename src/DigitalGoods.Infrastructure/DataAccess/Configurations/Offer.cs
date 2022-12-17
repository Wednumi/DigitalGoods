using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalGoods.Infrastructure.DataAccess.Configurations
{
    public class Offer : IEntityTypeConfiguration<Core.Entities.Offer>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Offer> builder)
        {
            builder.Ignore("PropertyChanged");
        }
    }
}
