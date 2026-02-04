using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Cars;

/// <summary>
/// Represents a car brand/manufacturer.
/// </summary>
public class CarBrand : BaseEntity
{
    /// <summary>
    /// Brand name (e.g., "Toyota", "BMW", "Tesla").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Brand logo URL (CDN/S3).
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? Logo { get; set; }

    /// <summary>
    /// Indicates if the brand is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation Properties

    /// <summary>
    /// Collection of cars for this brand.
    /// </summary>
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
