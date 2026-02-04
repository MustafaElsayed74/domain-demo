using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Cars;

/// <summary>
/// Represents a pricing tier for hourly car rental.
/// Allows tiered pricing based on rental duration.
/// Example: 0-30 hours @ $20/hour, 31-100 hours @ $15/hour, 100+ hours @ $10/hour
/// </summary>
public class CarPricingTier : BaseEntity
{
    /// <summary>
    /// Foreign key to the car.
    /// </summary>
    public long CarId { get; set; }

    /// <summary>
    /// Starting hour for this tier (inclusive).
    /// Example: 0 for the first tier.
    /// </summary>
    [Required]
    [Range(0, 10000)]
    public int FromHours { get; set; }

    /// <summary>
    /// Ending hour for this tier (inclusive).
    /// Example: 30 for "0-30 hours" tier.
    /// Use a high value like 9999 for "unlimited" last tier.
    /// </summary>
    [Required]
    [Range(0, 100000)]
    public int ToHours { get; set; }

    /// <summary>
    /// Price per hour in this tier.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 99999.99)]
    public decimal PricePerHour { get; set; }

    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Display order for the tier.
    /// </summary>
    public int DisplayOrder { get; set; } = 0;

    /// <summary>
    /// Optional tier name/description.
    /// </summary>
    [MaxLength(100)]
    public string? Name { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent car.
    /// </summary>
    public virtual Car Car { get; set; } = null!;

    // Helper Methods

    /// <summary>
    /// Calculates the price for a given number of hours in this tier.
    /// </summary>
    public decimal CalculatePrice(int hours)
    {
        if (hours <= 0) return 0;

        // Calculate hours applicable to this tier
        var applicableHours = Math.Min(hours, ToHours - FromHours + 1);
        return applicableHours * PricePerHour;
    }

    /// <summary>
    /// Checks if the given hour falls within this tier.
    /// </summary>
    public bool ContainsHour(int hour) => hour >= FromHours && hour <= ToHours;
}
