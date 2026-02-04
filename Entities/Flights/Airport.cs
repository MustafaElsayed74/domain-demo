using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Flights;

/// <summary>
/// Represents an airport for flight origin/destination.
/// </summary>
public class Airport : BaseEntity
{
    /// <summary>
    /// IATA airport code (e.g., "JFK", "DXB", "LHR").
    /// Must be unique.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Full airport name.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Airport city.
    /// Indexed for search.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// ISO country code.
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
    /// Timezone of the airport (IANA format, e.g., "America/New_York").
    /// </summary>
    [MaxLength(100)]
    public string? Timezone { get; set; }

    /// <summary>
    /// JSON array of airport amenities/facilities.
    /// Example: ["WiFi", "Lounge", "Duty Free"]
    /// </summary>
    public string? FacilitiesJson { get; set; }

    /// <summary>
    /// Indicates if the airport is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation Properties

    /// <summary>
    /// Flights departing from this airport.
    /// </summary>
    public virtual ICollection<Flight> DepartingFlights { get; set; } = new List<Flight>();

    /// <summary>
    /// Flights arriving at this airport.
    /// </summary>
    public virtual ICollection<Flight> ArrivingFlights { get; set; } = new List<Flight>();
}
