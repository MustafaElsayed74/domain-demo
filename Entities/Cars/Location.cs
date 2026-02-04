using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Cars;

/// <summary>
/// Represents a pickup/dropoff location for car rentals.
/// </summary>
public class Location : BaseEntity
{
    /// <summary>
    /// Location name (e.g., "JFK Airport", "Downtown Manhattan").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Full street address.
    /// </summary>
    [MaxLength(500)]
    public string? Address { get; set; }

    /// <summary>
    /// City name. Indexed for search.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Country name.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// GPS latitude coordinate.
    /// </summary>
    [Column(TypeName = "decimal(10,8)")]
    public decimal? Latitude { get; set; }

    /// <summary>
    /// GPS longitude coordinate.
    /// </summary>
    [Column(TypeName = "decimal(11,8)")]
    public decimal? Longitude { get; set; }

    /// <summary>
    /// JSON object with operating hours by day.
    /// Example: {"monday": "08:00-20:00", "tuesday": "08:00-20:00"}
    /// </summary>
    public string? OperatingHoursJson { get; set; }

    /// <summary>
    /// Phone number for the location.
    /// </summary>
    [MaxLength(50)]
    public string? Phone { get; set; }

    /// <summary>
    /// Email for the location.
    /// </summary>
    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Indicates if the location is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation Properties

    /// <summary>
    /// Collection of cars at this location.
    /// </summary>
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
