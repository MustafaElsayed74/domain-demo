namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the status of a tour schedule.
/// </summary>
public enum TourScheduleStatus
{
    /// <summary>
    /// Schedule is active and accepting bookings.
    /// </summary>
    Active = 0,

    /// <summary>
    /// Schedule is fully booked (available_slots = 0).
    /// </summary>
    Full = 1,

    /// <summary>
    /// Schedule has been cancelled.
    /// </summary>
    Cancelled = 2
}
