using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class TourScheduleConfiguration : IEntityTypeConfiguration<TourSchedule>
{
    public void Configure(EntityTypeBuilder<TourSchedule> builder)
    {
        builder.HasIndex(e => new { e.TourId, e.StartDate });
        builder.Property(e => e.RowVersion).IsRowVersion();
        builder.Ignore(e => e.IsValid);
    }
}
