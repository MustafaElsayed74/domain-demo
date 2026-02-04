using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Cars;

/// <summary>
/// Represents a rental car in the marketplace.
/// </summary>
public class Car : BaseEntity
{
    /// <summary>
    /// Foreign key to the car brand.
    /// </summary>
    public long BrandId { get; set; }

    /// <summary>
    /// Car manufacturer (e.g., "Toyota").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Make { get; set; } = string.Empty;

    /// <summary>
    /// Car model name (e.g., "Camry", "Model 3").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Car category.
    /// </summary>
    [Required]
    public CarCategory Category { get; set; }

    /// <summary>
    /// Number of passenger seats (2-9).
    /// </summary>
    [Required]
    [Range(2, 9)]
    public int SeatsCount { get; set; }

    /// <summary>
    /// Number of doors (2, 4, or 5).
    /// </summary>
    [Required]
    [Range(2, 5)]
    public int DoorsCount { get; set; }

    /// <summary>
    /// Fuel type.
    /// </summary>
    [Required]
    public FuelType FuelType { get; set; }

    /// <summary>
    /// Transmission type.
    /// </summary>
    [Required]
    public TransmissionType Transmission { get; set; }

    /// <summary>
    /// Number of large suitcases that fit in the trunk.
    /// </summary>
    public int? LuggageCapacity { get; set; }

    /// <summary>
    /// Indicates if the car has air conditioning.
    /// </summary>
    public bool AirConditioning { get; set; } = true;

    /// <summary>
    /// JSON array of car features.
    /// Example: ["GPS", "Bluetooth", "Backup Camera", "Sunroof"]
    /// </summary>
    public string? FeaturesJson { get; set; }

    /// <summary>
    /// JSON object with license requirements.
    /// Example: {"min_age": 21, "min_license_years": 2}
    /// </summary>
    public string? LicenseRequirementsJson { get; set; }

    /// <summary>
    /// Foreign key to the pickup location.
    /// </summary>
    public long LocationId { get; set; }

    /// <summary>
    /// JSON array of blocked/unavailable dates.
    /// Example: ["2026-02-15", "2026-02-16"]
    /// </summary>
    public string? AvailabilityCalendarJson { get; set; }

    /// <summary>
    /// Cancellation policy text.
    /// </summary>
    public string? CancellationPolicy { get; set; }

    /// <summary>
    /// Calculated average rating (0.00-5.00).
    /// </summary>
    [Column(TypeName = "decimal(3,2)")]
    public decimal? AvgRating { get; set; }

    /// <summary>
    /// Total number of reviews.
    /// </summary>
    public int ReviewCount { get; set; } = 0;

    /// <summary>
    /// User ID who created this car listing.
    /// </summary>
    public long? CreatedById { get; set; }

    /// <summary>
    /// User ID who last updated this car listing.
    /// </summary>
    public long? UpdatedById { get; set; }

    /// <summary>
    /// Indicates if the car is active and available.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// License plate number (for internal tracking).
    /// </summary>
    [MaxLength(50)]
    public string? LicensePlate { get; set; }

    /// <summary>
    /// Year of manufacture.
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Car color.
    /// </summary>
    [MaxLength(50)]
    public string? Color { get; set; }

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// The car brand.
    /// </summary>
    public virtual CarBrand Brand { get; set; } = null!;

    /// <summary>
    /// The pickup location.
    /// </summary>
    public virtual Location Location { get; set; } = null!;

    /// <summary>
    /// User who created this car listing.
    /// </summary>
    public virtual User? CreatedBy { get; set; }

    /// <summary>
    /// User who last updated this car listing.
    /// </summary>
    public virtual User? UpdatedBy { get; set; }

    /// <summary>
    /// Collection of pricing tiers for this car.
    /// </summary>
    public virtual ICollection<CarPricingTier> PricingTiers { get; set; } = new List<CarPricingTier>();

    /// <summary>
    /// Collection of images for this car.
    /// </summary>
    public virtual ICollection<CarImage> Images { get; set; } = new List<CarImage>();

    /// <summary>
    /// Collection of extras available for this car.
    /// </summary>
    public virtual ICollection<CarExtra> Extras { get; set; } = new List<CarExtra>();
}
