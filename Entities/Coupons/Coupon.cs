using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Coupons;

/// <summary>
/// Represents a discount coupon that can be applied to bookings.
/// </summary>
public class Coupon : BaseEntity
{
    /// <summary>
    /// Unique coupon code (uppercase alphanumeric).
    /// Example: "SUMMER2026"
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Coupon description/details.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Type of discount (percentage or fixed amount).
    /// </summary>
    [Required]
    public DiscountType DiscountType { get; set; }

    /// <summary>
    /// Discount value.
    /// For percentage: 20 = 20% off.
    /// For fixed: 50.00 = $50 off.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal DiscountValue { get; set; }

    /// <summary>
    /// Maximum discount cap for percentage discounts.
    /// Null means no cap.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? MaxDiscount { get; set; }

    /// <summary>
    /// Minimum order value required to use this coupon.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? MinPurchase { get; set; }

    /// <summary>
    /// Coupon validity start date.
    /// </summary>
    [Required]
    public DateOnly ValidFrom { get; set; }

    /// <summary>
    /// Coupon expiry date.
    /// </summary>
    [Required]
    public DateOnly ValidUntil { get; set; }

    /// <summary>
    /// Total usage limit. Null means unlimited.
    /// </summary>
    public int? UsageLimit { get; set; }

    /// <summary>
    /// Times the coupon has been used.
    /// </summary>
    public int UsedCount { get; set; } = 0;

    /// <summary>
    /// JSON array of applicable categories.
    /// Example: ["tours", "hotels"]
    /// Null means applicable to all categories.
    /// </summary>
    public string? ApplicableCategoriesJson { get; set; }

    /// <summary>
    /// Enable/disable flag.
    /// </summary>
    public bool Active { get; set; } = true;

    /// <summary>
    /// Maximum usage per user. Null means unlimited.
    /// </summary>
    public int? UsageLimitPerUser { get; set; }

    /// <summary>
    /// User ID who created this coupon.
    /// </summary>
    public long? CreatedById { get; set; }

    // Navigation Properties

    /// <summary>
    /// User who created this coupon.
    /// </summary>
    public virtual User? CreatedBy { get; set; }

    /// <summary>
    /// Collection of usage records.
    /// </summary>
    public virtual ICollection<CouponUsage> Usages { get; set; } = new List<CouponUsage>();

    // Helper Methods

    /// <summary>
    /// Checks if the coupon is currently valid.
    /// </summary>
    public bool IsValid
    {
        get
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return Active && 
                   today >= ValidFrom && 
                   today <= ValidUntil &&
                   (!UsageLimit.HasValue || UsedCount < UsageLimit.Value);
        }
    }

    /// <summary>
    /// Calculates the discount amount for a given subtotal.
    /// </summary>
    public decimal CalculateDiscount(decimal subtotal)
    {
        if (MinPurchase.HasValue && subtotal < MinPurchase.Value)
            return 0;

        decimal calculatedDiscount;

        if (DiscountType == DiscountType.Percentage)
        {
            calculatedDiscount = subtotal * (DiscountValue / 100);
            if (MaxDiscount.HasValue)
                calculatedDiscount = Math.Min(calculatedDiscount, MaxDiscount.Value);
        }
        else // Fixed
        {
            calculatedDiscount = Math.Min(DiscountValue, subtotal);
        }

        return calculatedDiscount;
    }

    /// <summary>
    /// Validates if the coupon can be applied.
    /// </summary>
    public (bool CanApply, string? ErrorMessage) CanApply(decimal subtotal, string? category = null)
    {
        if (!Active)
            return (false, "Coupon is not active");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        
        if (today < ValidFrom)
            return (false, "Coupon is not yet valid");

        if (today > ValidUntil)
            return (false, "Coupon has expired");

        if (UsageLimit.HasValue && UsedCount >= UsageLimit.Value)
            return (false, "Coupon usage limit reached");

        if (MinPurchase.HasValue && subtotal < MinPurchase.Value)
            return (false, $"Minimum purchase of {MinPurchase.Value} required");

        // Check category applicability if specified
        if (!string.IsNullOrEmpty(ApplicableCategoriesJson) && !string.IsNullOrEmpty(category))
        {
            var categories = System.Text.Json.JsonSerializer.Deserialize<string[]>(ApplicableCategoriesJson);
            if (categories != null && !categories.Contains(category, StringComparer.OrdinalIgnoreCase))
                return (false, "Coupon not applicable to this category");
        }

        return (true, null);
    }
}
