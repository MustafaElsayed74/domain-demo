using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class FlightFareConfiguration : IEntityTypeConfiguration<FlightFare>
{
    public void Configure(EntityTypeBuilder<FlightFare> builder)
    {
        builder.HasIndex(e => new { e.FlightId, e.CabinClass });
        builder.Property(e => e.RowVersion).IsRowVersion();
        builder.Ignore(e => e.TotalPrice);
    }
}
