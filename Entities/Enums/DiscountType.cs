namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the type of discount offered by a coupon.
/// </summary>
public enum DiscountType
{
    /// <summary>
    /// Percentage discount (e.g., 20% off).
    /// </summary>
    Percentage = 0,

    /// <summary>
    /// Fixed amount discount (e.g., $50 off).
    /// </summary>
    Fixed = 1
}
