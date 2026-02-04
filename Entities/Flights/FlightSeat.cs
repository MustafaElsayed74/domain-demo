using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Flights;

/// <summary>
/// Represents an individual seat on a flight.
/// Supports seat selection with premium charges and features.
/// </summary>
public class FlightSeat : BaseEntity
{
    /// <summary>
    /// Foreign key to the flight.
    /// </summary>
    public long FlightId { get; set; }

    /// <summary>
    /// Seat label/identifier (e.g., "1A", "12F", "23D").
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string SeatLabel { get; set; } = string.Empty;

    /// <summary>
    /// Cabin class of the seat.
    /// </summary>
    [Required]
    public CabinClass CabinClass { get; set; }

    /// <summary>
    /// JSON array of seat features.
    /// Example: ["window", "extra_legroom", "aisle"]
    /// </summary>
    public string? SeatFeaturesJson { get; set; }

    /// <summary>
    /// Indicates if the seat is currently available.
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Additional charge for preferred/premium seats.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? ExtraCharge { get; set; }

    /// <summary>
    /// Timestamp when the seat was temporarily locked for booking.
    /// Seats are auto-released after 15 minutes if booking not completed.
    /// </summary>
    public DateTime? LockedUntil { get; set; }

    /// <summary>
    /// User ID who has locked this seat during booking process.
    /// </summary>
    public long? LockedByUserId { get; set; }

    /// <summary>
    /// Row number (extracted from SeatLabel for sorting).
    /// </summary>
    public int? RowNumber { get; set; }

    /// <summary>
    /// Seat position in row (A, B, C, etc.).
    /// </summary>
    [MaxLength(5)]
    public string? Position { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent flight.
    /// </summary>
    public virtual Flight Flight { get; set; } = null!;

    /// <summary>
    /// User who has locked this seat.
    /// </summary>
    public virtual User? LockedByUser { get; set; }

    // Business Logic

    /// <summary>
    /// Checks if the seat can be selected (available and not locked by another user).
    /// </summary>
    public bool CanSelect(long? userId) => 
        IsAvailable && 
        (!LockedUntil.HasValue || LockedUntil < DateTime.UtcNow || LockedByUserId == userId);

    /// <summary>
    /// Locks the seat for the checkout process (15 minutes).
    /// </summary>
    public void Lock(long userId)
    {
        LockedUntil = DateTime.UtcNow.AddMinutes(15);
        LockedByUserId = userId;
    }

    /// <summary>
    /// Releases the seat lock.
    /// </summary>
    public void ReleaseLock()
    {
        LockedUntil = null;
        LockedByUserId = null;
    }

    /// <summary>
    /// Marks the seat as booked.
    /// </summary>
    public void Book()
    {
        IsAvailable = false;
        ReleaseLock();
    }

    /// <summary>
    /// Releases a booked seat (e.g., on cancellation).
    /// </summary>
    public void Release()
    {
        IsAvailable = true;
    }
}
