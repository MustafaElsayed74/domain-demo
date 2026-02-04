using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Tours;

/// <summary>
/// Represents a tour product in the travel marketplace.
/// Tours include activities, day trips, and multi-day excursions.
/// </summary>
public class Tour : BaseEntity
{
    /// <summary>
    /// Tour title/name.
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly identifier for the tour.
    /// Auto-generated from title, must be unique.
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Primary tour image URL (S3/CDN).
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? MainImage { get; set; }

    /// <summary>
    /// Short description (max 200 characters for previews).
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Complete tour details with HTML support.
    /// </summary>
    public string? FullDescription { get; set; }

    /// <summary>
    /// Tour duration in days.
    /// </summary>
    [Required]
    [Range(1, 365)]
    public int DurationDays { get; set; }

    /// <summary>
    /// Tour location (city/region).
    /// Indexed for search.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// JSON array of key tour highlights.
    /// Example: ["Eiffel Tower", "Louvre Museum"]
    /// </summary>
    public string? HighlightsJson { get; set; }

    /// <summary>
    /// JSON array of included activities.
    /// </summary>
    public string? ActivitiesJson { get; set; }

    /// <summary>
    /// JSON array of items included in the price.
    /// Example: ["meals", "transport", "guide"]
    /// </summary>
    public string? InclusiveItemsJson { get; set; }

    /// <summary>
    /// JSON array of items NOT included.
    /// Example: ["flights", "personal expenses"]
    /// </summary>
    public string? ExclusiveItemsJson { get; set; }

    /// <summary>
    /// Cancellation and refund policy text.
    /// </summary>
    public string? CancellationPolicy { get; set; }

    /// <summary>
    /// JSON array of available languages.
    /// Example: ["English", "French", "Spanish"]
    /// </summary>
    public string? LanguagesJson { get; set; }

    /// <summary>
    /// Physical difficulty level of the tour.
    /// </summary>
    public TourDifficulty? Difficulty { get; set; }

    /// <summary>
    /// JSON object with tour operator/provider details.
    /// </summary>
    public string? ProviderInfoJson { get; set; }

    /// <summary>
    /// JSON array of searchable tags.
    /// Example: ["adventure", "family-friendly", "cultural"]
    /// </summary>
    public string? TagsJson { get; set; }

    /// <summary>
    /// Indicates if this tour is featured/promoted.
    /// </summary>
    public bool Recommended { get; set; } = false;

    /// <summary>
    /// Calculated average rating (0.00-5.00).
    /// Updated when reviews are added/modified.
    /// </summary>
    [Column(TypeName = "decimal(3,2)")]
    public decimal? AvgRating { get; set; }

    /// <summary>
    /// Total number of reviews for this tour.
    /// </summary>
    public int ReviewCount { get; set; } = 0;

    /// <summary>
    /// User ID who created this tour.
    /// </summary>
    public long? CreatedById { get; set; }

    /// <summary>
    /// User ID who last updated this tour.
    /// </summary>
    public long? UpdatedById { get; set; }

    /// <summary>
    /// Indicates if the tour is active and visible.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// User who created this tour.
    /// </summary>
    public virtual User? CreatedBy { get; set; }

    /// <summary>
    /// User who last updated this tour.
    /// </summary>
    public virtual User? UpdatedBy { get; set; }

    /// <summary>
    /// Collection of schedules for this tour.
    /// </summary>
    public virtual ICollection<TourSchedule> Schedules { get; set; } = new List<TourSchedule>();

    /// <summary>
    /// Collection of price tiers for this tour.
    /// </summary>
    public virtual ICollection<TourPriceTier> PriceTiers { get; set; } = new List<TourPriceTier>();

    /// <summary>
    /// Collection of images for this tour.
    /// </summary>
    public virtual ICollection<TourImage> Images { get; set; } = new List<TourImage>();
}
