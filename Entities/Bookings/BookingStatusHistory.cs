using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Bookings;

/// <summary>
/// Represents an immutable log entry for booking status changes.
/// INSERT only - no UPDATE or DELETE allowed.
/// </summary>
public class BookingStatusHistory : BaseEntity
{
    /// <summary>
    /// Foreign key to the booking.
    /// </summary>
    public long BookingId { get; set; }

    /// <summary>
    /// Previous status (null for initial creation).
    /// </summary>
    [MaxLength(50)]
    public string? OldStatus { get; set; }

    /// <summary>
    /// New status after the change.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string NewStatus { get; set; } = string.Empty;

    /// <summary>
    /// Reason for the status change.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// User who made the change (null for system changes).
    /// </summary>
    public long? ChangedById { get; set; }

    /// <summary>
    /// IP address of the user who made the change (for audit).
    /// </summary>
    [MaxLength(50)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// User agent string (for audit).
    /// </summary>
    [MaxLength(500)]
    public string? UserAgent { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent booking.
    /// </summary>
    public virtual Booking Booking { get; set; } = null!;

    /// <summary>
    /// User who made the change.
    /// </summary>
    public virtual User? ChangedBy { get; set; }

    // Factory Method

    /// <summary>
    /// Creates a new status history entry.
    /// </summary>
    public static BookingStatusHistory Create(
        long bookingId,
        string? oldStatus,
        string newStatus,
        string? reason = null,
        long? changedById = null,
        string? ipAddress = null,
        string? userAgent = null)
    {
        return new BookingStatusHistory
        {
            BookingId = bookingId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            Reason = reason,
            ChangedById = changedById,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
