using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class CarExtraConfiguration : IEntityTypeConfiguration<CarExtra>
{
    public void Configure(EntityTypeBuilder<CarExtra> builder)
    {
        builder.Ignore(e => e.IsValid);
    }
}
