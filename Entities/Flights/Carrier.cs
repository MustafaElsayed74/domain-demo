using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Flights;

/// <summary>
/// Represents an airline carrier.
/// </summary>
public class Carrier : BaseEntity
{
    /// <summary>
    /// Airline name (e.g., "Emirates", "Delta").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// IATA airline code (e.g., "EK", "DL").
    /// Must be unique.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Airline logo URL (CDN/S3).
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? Logo { get; set; }

    /// <summary>
    /// JSON object with contact information.
    /// Example: {"phone": "+1234", "email": "support@airline.com"}
    /// </summary>
    public string? ContactInfoJson { get; set; }

    /// <summary>
    /// Indicates if the carrier is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation Properties

    /// <summary>
    /// Collection of flights operated by this carrier.
    /// </summary>
    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
