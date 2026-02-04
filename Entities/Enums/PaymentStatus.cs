namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the payment status of a booking.
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Payment is pending.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Payment has been successfully processed.
    /// </summary>
    Paid = 1,

    /// <summary>
    /// Payment has failed.
    /// </summary>
    Failed = 2,

    /// <summary>
    /// Payment has been refunded.
    /// </summary>
    Refunded = 3
}
