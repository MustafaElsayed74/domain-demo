using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasIndex(e => new { e.OriginAirportId, e.DestinationAirportId, e.DepartureAt });
        builder.Property(e => e.RowVersion).IsRowVersion();
        builder.Ignore(e => e.IsValid);
        builder.Ignore(e => e.StopCount);

        // Configure relationships to airports
        builder.HasOne(e => e.OriginAirport)
            .WithMany(a => a.DepartingFlights)
            .HasForeignKey(e => e.OriginAirportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.DestinationAirport)
            .WithMany(a => a.ArrivingFlights)
            .HasForeignKey(e => e.DestinationAirportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
