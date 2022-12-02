using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalGoods.Infrastructure.DataAccess.Configurations
{
    public class Source : IEntityTypeConfiguration<Core.Entities.Source>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Source> builder)
        {
            builder.HasOne(c => c.Parent)
                .WithMany(p => p.Childs)
                .HasForeignKey(c => c.ParentId);
        }
    }
}
