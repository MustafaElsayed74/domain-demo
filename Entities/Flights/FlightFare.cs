using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Flights;

/// <summary>
/// Represents pricing for a specific cabin class on a flight.
/// Supports dynamic pricing based on availability.
/// </summary>
public class FlightFare : BaseEntity
{
    /// <summary>
    /// Foreign key to the flight.
    /// </summary>
    public long FlightId { get; set; }

    /// <summary>
    /// Cabin class for this fare.
    /// </summary>
    [Required]
    public CabinClass CabinClass { get; set; }

    /// <summary>
    /// Base ticket price before taxes and fees.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 999999.99)]
    public decimal BasePrice { get; set; }

    /// <summary>
    /// Government taxes.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, 999999.99)]
    public decimal Taxes { get; set; }

    /// <summary>
    /// Airline fees and surcharges.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, 999999.99)]
    public decimal Fees { get; set; }

    /// <summary>
    /// ISO 4217 currency code (e.g., 'USD', 'EUR').
    /// </summary>
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Number of available seats in this class.
    /// Decremented atomically on booking.
    /// </summary>
    [Required]
    [Range(0, 10000)]
    public int SeatsAvailable { get; set; }

    /// <summary>
    /// Foreign key to the fare rules.
    /// </summary>
    public long? FareRulesId { get; set; }

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent flight.
    /// </summary>
    public virtual Flight Flight { get; set; } = null!;

    /// <summary>
    /// The fare rules for this fare.
    /// </summary>
    public virtual FareRule? FareRules { get; set; }

    // Calculated Properties

    /// <summary>
    /// Total price including base, taxes, and fees.
    /// </summary>
    public decimal TotalPrice => BasePrice + Taxes + Fees;

    // Business Logic

    /// <summary>
    /// Attempts to reserve seats for booking.
    /// Returns false if insufficient seats available.
    /// </summary>
    public bool TryReserveSeats(int count)
    {
        if (SeatsAvailable < count)
            return false;

        SeatsAvailable -= count;
        return true;
    }

    /// <summary>
    /// Releases reserved seats (e.g., on booking cancellation).
    /// </summary>
    public void ReleaseSeats(int count)
    {
        SeatsAvailable += count;
    }
}
