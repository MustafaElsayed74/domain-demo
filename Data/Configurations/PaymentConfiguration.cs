using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMarketplace.Api.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasIndex(e => e.TransactionId).IsUnique();
        builder.HasIndex(e => e.BookingId);
        builder.Property(e => e.RowVersion).IsRowVersion();
        builder.Ignore(e => e.CanRefund);
        builder.Ignore(e => e.RefundableAmount);
    }
}
