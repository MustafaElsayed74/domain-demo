using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Tours;

/// <summary>
/// Represents an image associated with a tour.
/// Maximum 20 images per tour, supports multiple sizes and formats.
/// </summary>
public class TourImage : BaseEntity
{
    /// <summary>
    /// Foreign key to the parent tour.
    /// </summary>
    public long TourId { get; set; }

    /// <summary>
    /// CDN/S3 URL for the image.
    /// Supports WebP and AVIF formats for optimization.
    /// </summary>
    [Required]
    [MaxLength(500)]
    [Url]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Optional thumbnail URL (smaller size).
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? ThumbnailUrl { get; set; }

    /// <summary>
    /// Optional medium size URL.
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? MediumUrl { get; set; }

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

    /// <summary>
    /// Display order for the image.
    /// Lower numbers display first.
    /// </summary>
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// Original filename for reference.
    /// </summary>
    [MaxLength(255)]
    public string? OriginalFileName { get; set; }

    /// <summary>
    /// File size in bytes.
    /// </summary>
    public long? FileSizeBytes { get; set; }

    /// <summary>
    /// MIME type of the image (e.g., 'image/webp', 'image/avif').
    /// </summary>
    [MaxLength(50)]
    public string? MimeType { get; set; }

    /// <summary>
    /// Image width in pixels.
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Image height in pixels.
    /// </summary>
    public int? Height { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent tour.
    /// </summary>
    public virtual Tour Tour { get; set; } = null!;
}
