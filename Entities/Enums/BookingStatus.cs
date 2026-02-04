namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the status of a booking.
/// </summary>
public enum BookingStatus
{
    /// <summary>
    /// Booking is pending confirmation/payment.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Booking is confirmed.
    /// </summary>
    Confirmed = 1,

    /// <summary>
    /// Booking has been cancelled.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Booking has been completed (service delivered).
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Booking has been refunded.
    /// </summary>
    Refunded = 4
}
