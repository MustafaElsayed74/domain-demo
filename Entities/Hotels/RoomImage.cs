using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Hotels;

/// <summary>
/// Represents an image of a hotel room.
/// </summary>
public class RoomImage : BaseEntity
{
    /// <summary>
    /// Foreign key to the room.
    /// </summary>
    public long RoomId { get; set; }

    /// <summary>
    /// CDN/S3 URL for the image.
    /// </summary>
    [Required]
    [MaxLength(500)]
    [Url]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Thumbnail URL (smaller size).
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? ThumbnailUrl { get; set; }

    /// <summary>
    /// Display order (lower numbers display first).
    /// </summary>
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// Image caption/description.
    /// </summary>
    [MaxLength(255)]
    public string? Caption { get; set; }

    /// <summary>
    /// Alt text for accessibility.
    /// </summary>
    [MaxLength(255)]
    public string? AltText { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent room.
    /// </summary>
    public virtual Room Room { get; set; } = null!;
}
