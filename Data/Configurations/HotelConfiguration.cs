using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.HasIndex(e => e.City);
        builder.Property(e => e.RowVersion).IsRowVersion();
    }
}
