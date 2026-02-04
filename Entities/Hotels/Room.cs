using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Hotels;

/// <summary>
/// Represents a room type within a hotel.
/// </summary>
public class Room : BaseEntity
{
    /// <summary>
    /// Foreign key to the hotel.
    /// </summary>
    public long HotelId { get; set; }

    /// <summary>
    /// Room type name (e.g., "Deluxe King", "Family Suite").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Room details and description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Maximum adult occupancy.
    /// </summary>
    [Required]
    [Range(1, 20)]
    public int MaxAdults { get; set; }

    /// <summary>
    /// Maximum children occupancy.
    /// </summary>
    [Required]
    [Range(0, 10)]
    public int MaxChildren { get; set; }

    /// <summary>
    /// Maximum infants (under 2 years).
    /// </summary>
    [Required]
    [Range(0, 5)]
    public int MaxInfants { get; set; }

    /// <summary>
    /// Bed configuration (e.g., "1 King", "2 Queens", "Twin Beds").
    /// </summary>
    [MaxLength(100)]
    public string? BedType { get; set; }

    /// <summary>
    /// Room size in square meters.
    /// </summary>
    public int? RoomAreaSqm { get; set; }

    /// <summary>
    /// Base nightly rate.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 999999.99)]
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// JSON array of blocked/unavailable dates.
    /// Example: ["2026-02-15", "2026-02-16"]
    /// </summary>
    public string? AvailabilityCalendarJson { get; set; }

    /// <summary>
    /// Indicates if the booking is refundable.
    /// </summary>
    public bool Refundable { get; set; } = false;

    /// <summary>
    /// JSON array of room-specific amenities.
    /// Example: ["Balcony", "Sea View", "Kitchen"]
    /// </summary>
    public string? ExtrasJson { get; set; }

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
    /// User ID who created this room.
    /// </summary>
    public long? CreatedById { get; set; }

    /// <summary>
    /// User ID who last updated this room.
    /// </summary>
    public long? UpdatedById { get; set; }

    /// <summary>
    /// Total inventory count (number of rooms of this type).
    /// </summary>
    [Range(1, 1000)]
    public int TotalInventory { get; set; } = 1;

    /// <summary>
    /// Minimum stay requirement in nights.
    /// </summary>
    public int? MinimumStayNights { get; set; }

    /// <summary>
    /// Indicates if the room is active and bookable.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent hotel.
    /// </summary>
    public virtual Hotel Hotel { get; set; } = null!;

    /// <summary>
    /// User who created this room.
    /// </summary>
    public virtual User? CreatedBy { get; set; }

    /// <summary>
    /// User who last updated this room.
    /// </summary>
    public virtual User? UpdatedBy { get; set; }

    /// <summary>
    /// Collection of images for this room.
    /// </summary>
    public virtual ICollection<RoomImage> Images { get; set; } = new List<RoomImage>();

    // Helper Properties

    /// <summary>
    /// Total maximum occupancy.
    /// </summary>
    public int TotalMaxOccupancy => MaxAdults + MaxChildren + MaxInfants;
}
