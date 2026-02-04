using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMarketplace.Api.Entities.Coupons;

/// <summary>
/// Represents a record of coupon usage on a booking.
/// </summary>
public class CouponUsage
{
    /// <summary>
    /// Primary key identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Foreign key to the coupon used.
    /// </summary>
    public long CouponId { get; set; }

    /// <summary>
    /// Foreign key to the booking where coupon was applied.
    /// One coupon per booking (unique constraint).
    /// </summary>
    public long BookingId { get; set; }

    /// <summary>
    /// Foreign key to the user who used the coupon.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Actual discount amount applied.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal DiscountApplied { get; set; }

    /// <summary>
    /// Timestamp when the coupon was used.
    /// </summary>
    [Required]
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    /// <summary>
    /// The coupon that was used.
    /// </summary>
    public virtual Coupon Coupon { get; set; } = null!;

    /// <summary>
    /// The booking where the coupon was applied.
    /// </summary>
    public virtual Booking Booking { get; set; } = null!;

    /// <summary>
    /// The user who used the coupon.
    /// </summary>
    public virtual User User { get; set; } = null!;
}
