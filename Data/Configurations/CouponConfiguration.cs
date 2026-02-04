using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasIndex(e => e.Code).IsUnique();
        builder.HasIndex(e => new { e.Active, e.ValidFrom, e.ValidUntil });
        builder.Ignore(e => e.IsValid);
    }
}
