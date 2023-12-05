using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resources.Core.Entities;
using Resources.Core.ValueObjects;

namespace Resources.Infrastructure.DAL.Configurations;
internal sealed class ResourceBlockadeConfiguration : IEntityTypeConfiguration<ResourceBlockade>
{
    public void Configure(EntityTypeBuilder<ResourceBlockade> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ResourceBlockadeId(x));
        builder.Property(x => x.ResourceId)
            .HasConversion(x => x.Value, x => new ResourceId(x));
    }
}
