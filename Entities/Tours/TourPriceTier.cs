using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Tours;

/// <summary>
/// Represents a pricing tier for a tour.
/// Each tour must have at least one price tier.
/// </summary>
public class TourPriceTier : BaseEntity
{
    /// <summary>
    /// Foreign key to the parent tour.
    /// </summary>
    public long TourId { get; set; }

    /// <summary>
    /// Tier name (e.g., 'Standard', 'Premium', 'VIP').
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Price per adult participant.
    /// Must be positive.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 999999.99)]
    public decimal AdultPrice { get; set; }

    /// <summary>
    /// Price per child (ages 2-12).
    /// Must be positive or zero.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, 999999.99)]
    public decimal ChildPrice { get; set; }

    /// <summary>
    /// Price per infant (ages 0-2).
    /// Must be positive or zero.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, 999999.99)]
    public decimal InfantPrice { get; set; }

    /// <summary>
    /// ISO 4217 currency code (e.g., 'USD', 'EUR', 'GBP').
    /// </summary>
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Optional description of what this tier includes.
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Display order for the tier.
    /// </summary>
    public int DisplayOrder { get; set; } = 0;

    /// <summary>
    /// Indicates if this tier is currently available.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation Properties

    /// <summary>
    /// The parent tour.
    /// </summary>
    public virtual Tour Tour { get; set; } = null!;

    /// <summary>
    /// Schedules using this price tier.
    /// </summary>
    public virtual ICollection<TourSchedule> Schedules { get; set; } = new List<TourSchedule>();

    // Helper Methods

    /// <summary>
    /// Calculates total price for a group.
    /// </summary>
    public decimal CalculateTotal(int adults, int children, int infants)
    {
        return (AdultPrice * adults) + (ChildPrice * children) + (InfantPrice * infants);
    }
}
