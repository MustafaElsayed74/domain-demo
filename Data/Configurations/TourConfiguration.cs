using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class TourConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.HasIndex(e => e.Location);
        builder.Property(e => e.RowVersion).IsRowVersion();
    }
}
