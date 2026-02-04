using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasIndex(e => new { e.UserId, e.Category, e.ItemId }).IsUnique();
        builder.HasIndex(e => new { e.Category, e.ItemId, e.Status });
        builder.Ignore(e => e.IsVisible);
        builder.Ignore(e => e.IsVerified);

        // Avoid cascade cycle with User
        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
