using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Core;

/// <summary>
/// Represents a product category in the travel marketplace.
/// FIXED 4 categories: Tours, Flights, Cars, Hotels.
/// Categories are immutable (no INSERT/DELETE allowed at database level).
/// Only UPDATE operations permitted on editable fields.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// System identifier for the category (e.g., 'tours', 'flights', 'cars', 'hotels').
    /// Used for programmatic access and routing.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Display name for the category (translatable).
    /// This field is editable by admin.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Category description text.
    /// This field is editable by admin.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// URL to the category banner image (S3/CDN URL).
    /// This field is editable by admin.
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? Image { get; set; }

    /// <summary>
    /// JSON configuration defining which fields admin can edit for products in this category.
    /// Schema varies per category type.
    /// </summary>
    public string? EditableFieldsJson { get; set; }

    /// <summary>
    /// Indicates if this category is active and visible to users.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Display order for the category in UI listings.
    /// </summary>
    public int DisplayOrder { get; set; } = 0;

    // Static category keys for type-safe access
    public static class Keys
    {
        public const string Tours = "tours";
        public const string Flights = "flights";
        public const string Cars = "cars";
        public const string Hotels = "hotels";
    }

    /// <summary>
    /// Predefined categories for seeding the database.
    /// These are the ONLY 4 categories allowed in the system.
    /// </summary>
    public static IEnumerable<Category> GetSeedData()
    {
        // EF Core requires fixed DateTime values for seed data
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return new List<Category>
        {
            new()
            {
                Id = 1,
                Key = Keys.Tours,
                Title = "Tours & Activities",
                Description = "Explore exciting tours and activities around the world",
                DisplayOrder = 1,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new()
            {
                Id = 2,
                Key = Keys.Flights,
                Title = "Flights",
                Description = "Find and book flights to destinations worldwide",
                DisplayOrder = 2,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new()
            {
                Id = 3,
                Key = Keys.Cars,
                Title = "Car Rentals",
                Description = "Rent cars for your travel needs",
                DisplayOrder = 3,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new()
            {
                Id = 4,
                Key = Keys.Hotels,
                Title = "Hotels & Accommodations",
                Description = "Book hotels and accommodations for your stay",
                DisplayOrder = 4,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        };
    }
}
