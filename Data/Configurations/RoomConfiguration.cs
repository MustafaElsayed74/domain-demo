using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasIndex(e => new { e.HotelId, e.PricePerNight });
        builder.Property(e => e.RowVersion).IsRowVersion();
        builder.Ignore(e => e.TotalMaxOccupancy);
    }
}
