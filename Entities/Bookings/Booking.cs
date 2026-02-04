using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Bookings;

/// <summary>
/// Represents a polymorphic booking that can be for tours, flights, cars, or hotels.
/// </summary>
public class Booking : BaseEntity
{
    /// <summary>
    /// Unique booking reference number.
    /// Format: {CATEGORY_PREFIX}-{YEAR}-{SEQUENCE} (e.g., "TRV-2026-0001234")
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string BookingReference { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key to the user who made the booking.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Product category (tour, flight, car, hotel).
    /// </summary>
    [Required]
    public BookingCategory Category { get; set; }

    /// <summary>
    /// Polymorphic foreign key to the booked item.
    /// References tours.id, flights.id, cars.id, or rooms.id based on Category.
    /// </summary>
    public long ItemId { get; set; }

    /// <summary>
    /// Current booking status.
    /// </summary>
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    /// <summary>
    /// Pre-tax/fee amount.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Tax amount.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Taxes { get; set; } = 0;

    /// <summary>
    /// Service/booking fees.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Fees { get; set; } = 0;

    /// <summary>
    /// Discount applied from coupon.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Discount { get; set; } = 0;

    /// <summary>
    /// Final total amount (subtotal + taxes + fees - discount).
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Current payment status.
    /// </summary>
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    /// <summary>
    /// Customer's special requests or notes.
    /// </summary>
    public string? CustomerNotes { get; set; }

    /// <summary>
    /// Reason for cancellation (if cancelled).
    /// </summary>
    public string? CancellationReason { get; set; }

    /// <summary>
    /// Timestamp when the booking was made.
    /// </summary>
    [Required]
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Check-in date (for cars/hotels/tours).
    /// </summary>
    public DateTime? CheckInDate { get; set; }

    /// <summary>
    /// Check-out date (for cars/hotels).
    /// </summary>
    public DateTime? CheckOutDate { get; set; }

    /// <summary>
    /// User ID who created this booking (may differ from UserId for admin bookings).
    /// </summary>
    public long? CreatedById { get; set; }

    /// <summary>
    /// User ID who last updated this booking.
    /// </summary>
    public long? UpdatedById { get; set; }

    /// <summary>
    /// Timestamp when the booking was cancelled.
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// Timestamp when payment hold expires (auto-release after 15 minutes).
    /// </summary>
    public DateTime? HoldExpiresAt { get; set; }

    /// <summary>
    /// Foreign key to the coupon used (if any).
    /// </summary>
    public long? CouponId { get; set; }

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// The user who made the booking.
    /// </summary>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// User who created this booking.
    /// </summary>
    public virtual User? CreatedBy { get; set; }

    /// <summary>
    /// User who last updated this booking.
    /// </summary>
    public virtual User? UpdatedBy { get; set; }

    /// <summary>
    /// One-to-one relationship with booking details.
    /// </summary>
    public virtual BookingDetail? Detail { get; set; }

    /// <summary>
    /// Collection of status history entries.
    /// </summary>
    public virtual ICollection<BookingStatusHistory> StatusHistory { get; set; } = new List<BookingStatusHistory>();

    // Helper Methods

    /// <summary>
    /// Generates a booking reference.
    /// </summary>
    public static string GenerateReference(BookingCategory category, int sequence)
    {
        var prefix = category switch
        {
            BookingCategory.Tour => "TOR",
            BookingCategory.Flight => "FLT",
            BookingCategory.Car => "CAR",
            BookingCategory.Hotel => "HTL",
            _ => "TRV"
        };
        return $"{prefix}-{DateTime.UtcNow.Year}-{sequence:D7}";
    }

    /// <summary>
    /// Calculates total price from components.
    /// </summary>
    public void CalculateTotalPrice()
    {
        TotalPrice = Subtotal + Taxes + Fees - Discount;
    }

    /// <summary>
    /// Checks if booking can be cancelled based on check-in date.
    /// </summary>
    public bool CanCancel(int hoursBeforeCheckIn = 24)
    {
        if (Status == BookingStatus.Cancelled || Status == BookingStatus.Refunded)
            return false;

        if (!CheckInDate.HasValue)
            return true;

        return CheckInDate.Value > DateTime.UtcNow.AddHours(hoursBeforeCheckIn);
    }
}
