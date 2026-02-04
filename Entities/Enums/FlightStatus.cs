namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the status of a flight.
/// </summary>
public enum FlightStatus
{
    /// <summary>
    /// Flight is scheduled and operating normally.
    /// </summary>
    Scheduled = 0,

    /// <summary>
    /// Flight departure has been delayed.
    /// </summary>
    Delayed = 1,

    /// <summary>
    /// Flight has been cancelled.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Flight has completed its journey.
    /// </summary>
    Completed = 3
}
