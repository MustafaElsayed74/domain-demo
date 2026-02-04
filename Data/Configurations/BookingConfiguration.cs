using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasIndex(e => e.BookingReference).IsUnique();
        builder.HasIndex(e => new { e.UserId, e.Status });
        builder.HasIndex(e => new { e.Category, e.ItemId });
        builder.Property(e => e.RowVersion).IsRowVersion();

        // Avoid cascade cycle with User
        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
