using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Cars;

/// <summary>
/// Represents an optional extra/add-on for a car rental.
/// Examples: GPS, Child Seat, Additional Driver, Full Insurance.
/// </summary>
public class CarExtra : BaseEntity
{
    /// <summary>
    /// Foreign key to the car.
    /// </summary>
    public long CarId { get; set; }

    /// <summary>
    /// Extra name (e.g., "GPS", "Child Seat", "Additional Driver").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the extra.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Daily rate for the extra (charged per day of rental).
    /// Either PricePerDay or PricePerRental should be set, not both.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? PricePerDay { get; set; }

    /// <summary>
    /// One-time fee for the entire rental period.
    /// Either PricePerDay or PricePerRental should be set, not both.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? PricePerRental { get; set; }

    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Indicates if this extra is currently available.
    /// </summary>
    public bool Available { get; set; } = true;

    /// <summary>
    /// Maximum quantity available (e.g., only 3 child seats).
    /// Null means unlimited.
    /// </summary>
    public int? MaxQuantity { get; set; }

    /// <summary>
    /// Current available quantity.
    /// </summary>
    public int? AvailableQuantity { get; set; }

    /// <summary>
    /// Indicates if multiple units can be added to a single booking.
    /// </summary>
    public bool AllowMultiple { get; set; } = false;

    // Navigation Properties

    /// <summary>
    /// The parent car.
    /// </summary>
    public virtual Car Car { get; set; } = null!;

    // Business Logic

    /// <summary>
    /// Calculates the price for the extra based on rental days.
    /// </summary>
    public decimal CalculatePrice(int rentalDays, int quantity = 1)
    {
        if (PricePerRental.HasValue)
            return PricePerRental.Value * quantity;

        if (PricePerDay.HasValue)
            return PricePerDay.Value * rentalDays * quantity;

        return 0;
    }

    /// <summary>
    /// Validates that either PricePerDay or PricePerRental is set (not both).
    /// </summary>
    public bool IsValid => (PricePerDay.HasValue && !PricePerRental.HasValue) ||
                           (!PricePerDay.HasValue && PricePerRental.HasValue);
}
