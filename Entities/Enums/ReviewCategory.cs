namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the category of a reviewable product.
/// Extends BookingCategory with room-level reviews.
/// </summary>
public enum ReviewCategory
{
    /// <summary>
    /// Tour review.
    /// </summary>
    Tour = 0,

    /// <summary>
    /// Flight review.
    /// </summary>
    Flight = 1,

    /// <summary>
    /// Car rental review.
    /// </summary>
    Car = 2,

    /// <summary>
    /// Hotel review.
    /// </summary>
    Hotel = 3,

    /// <summary>
    /// Room-specific review (within a hotel).
    /// </summary>
    Room = 4
}
