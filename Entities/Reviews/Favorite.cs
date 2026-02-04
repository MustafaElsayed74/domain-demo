using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Reviews;

/// <summary>
/// Represents a user's favorite/wishlisted item.
/// Polymorphic design supporting all product categories.
/// </summary>
public class Favorite
{
    /// <summary>
    /// Primary key identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Foreign key to the user who favorited the item.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Product category of the favorited item.
    /// </summary>
    [Required]
    public ReviewCategory Category { get; set; }

    /// <summary>
    /// Polymorphic foreign key to the favorited item.
    /// References tours.id, flights.id, cars.id, hotels.id, or rooms.id.
    /// </summary>
    public long ItemId { get; set; }

    /// <summary>
    /// Timestamp when the item was favorited.
    /// </summary>
    [Required]
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Optional user notes about why they favorited this item.
    /// </summary>
    [MaxLength(500)]
    public string? Notes { get; set; }

    /// <summary>
    /// Optional collection/folder name for organizing favorites.
    /// </summary>
    [MaxLength(100)]
    public string? Collection { get; set; }

    // Navigation Properties

    /// <summary>
    /// The user who favorited the item.
    /// </summary>
    public virtual User User { get; set; } = null!;
}
