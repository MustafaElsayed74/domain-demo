using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Tours;

/// <summary>
/// Represents a scheduled instance of a tour with specific dates and capacity.
/// Uses optimistic locking to prevent overbooking.
/// </summary>
public class TourSchedule : BaseEntity
{
    /// <summary>
    /// Foreign key to the parent tour.
    /// </summary>
    public long TourId { get; set; }

    /// <summary>
    /// Tour start date.
    /// </summary>
    [Required]
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Tour end date.
    /// </summary>
    [Required]
    public DateOnly EndDate { get; set; }

    /// <summary>
    /// Maximum number of participants.
    /// </summary>
    [Required]
    [Range(1, 10000)]
    public int TotalCapacity { get; set; }

    /// <summary>
    /// Remaining available slots.
    /// Cannot exceed TotalCapacity.
    /// </summary>
    [Required]
    [Range(0, 10000)]
    public int AvailableSlots { get; set; }

    /// <summary>
    /// Foreign key to the price tier for this schedule.
    /// </summary>
    public long PriceTierId { get; set; }

    /// <summary>
    /// Current status of the schedule.
    /// Automatically set to 'Full' when AvailableSlots = 0.
    /// </summary>
    public TourScheduleStatus Status { get; set; } = TourScheduleStatus.Active;

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// Prevents overbooking through concurrent updates.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent tour.
    /// </summary>
    public virtual Tour Tour { get; set; } = null!;

    /// <summary>
    /// The price tier for this schedule.
    /// </summary>
    public virtual TourPriceTier PriceTier { get; set; } = null!;

    // Business Logic Methods

    /// <summary>
    /// Attempts to reserve slots for booking.
    /// Returns false if insufficient slots available.
    /// </summary>
    public bool TryReserveSlots(int count)
    {
        if (AvailableSlots < count || Status != TourScheduleStatus.Active)
            return false;

        AvailableSlots -= count;
        
        if (AvailableSlots == 0)
            Status = TourScheduleStatus.Full;

        return true;
    }

    /// <summary>
    /// Releases reserved slots (e.g., on booking cancellation).
    /// </summary>
    public void ReleaseSlots(int count)
    {
        AvailableSlots += count;
        
        if (AvailableSlots > TotalCapacity)
            AvailableSlots = TotalCapacity;

        if (Status == TourScheduleStatus.Full && AvailableSlots > 0)
            Status = TourScheduleStatus.Active;
    }

    /// <summary>
    /// Validates the schedule dates and capacity.
    /// </summary>
    public bool IsValid => StartDate <= EndDate && 
                           AvailableSlots <= TotalCapacity && 
                           AvailableSlots >= 0;
}
