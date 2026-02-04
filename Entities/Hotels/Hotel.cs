using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Hotels;

/// <summary>
/// Represents a hotel/accommodation in the marketplace.
/// </summary>
public class Hotel : BaseEntity
{
    /// <summary>
    /// Hotel name.
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly identifier for the hotel.
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Hotel description with HTML support.
    /// </summary>
    public string? Description { get; set; }

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
    /// Primary hotel image URL (CDN/S3).
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? MainImage { get; set; }

    /// <summary>
    /// JSON array of gallery image URLs.
    /// </summary>
    public string? GalleryJson { get; set; }

    /// <summary>
    /// JSON array of hotel amenities.
    /// Example: ["Pool", "Gym", "Spa", "Restaurant", "WiFi", "Parking"]
    /// </summary>
    public string? AmenitiesJson { get; set; }

    /// <summary>
    /// Official star rating (0.0-5.0).
    /// </summary>
    [Column(TypeName = "decimal(2,1)")]
    [Range(0, 5)]
    public decimal? StarRating { get; set; }

    /// <summary>
    /// JSON object with hotel policies.
    /// Example: {"check_in": "15:00", "check_out": "11:00", "cancellation": "..."}
    /// </summary>
    public string? PoliciesJson { get; set; }

    /// <summary>
    /// JSON object with contact information.
    /// Example: {"phone": "+1234", "email": "hotel@..."}
    /// </summary>
    public string? ContactInfoJson { get; set; }

    /// <summary>
    /// Calculated average user rating (0.00-5.00).
    /// </summary>
    [Column(TypeName = "decimal(3,2)")]
    public decimal? AvgRating { get; set; }

    /// <summary>
    /// Total number of user reviews.
    /// </summary>
    public int ReviewCount { get; set; } = 0;

    /// <summary>
    /// User ID who created this hotel.
    /// </summary>
    public long? CreatedById { get; set; }

    /// <summary>
    /// User ID who last updated this hotel.
    /// </summary>
    public long? UpdatedById { get; set; }

    /// <summary>
    /// Indicates if the hotel is active and visible.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// User who created this hotel.
    /// </summary>
    public virtual User? CreatedBy { get; set; }

    /// <summary>
    /// User who last updated this hotel.
    /// </summary>
    public virtual User? UpdatedBy { get; set; }

    /// <summary>
    /// Collection of rooms in this hotel.
    /// </summary>
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
