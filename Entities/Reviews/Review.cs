using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Reviews;

/// <summary>
/// Represents a user review for a product (tour, flight, car, hotel, room).
/// Polymorphic design supporting all product categories.
/// </summary>
public class Review : BaseEntity
{
    /// <summary>
    /// Foreign key to the user who wrote the review.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Product category being reviewed.
    /// </summary>
    [Required]
    public ReviewCategory Category { get; set; }

    /// <summary>
    /// Polymorphic foreign key to the reviewed item.
    /// References tours.id, flights.id, cars.id, hotels.id, or rooms.id.
    /// </summary>
    public long ItemId { get; set; }

    /// <summary>
    /// Star rating (1-5).
    /// </summary>
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    /// <summary>
    /// Review headline/title.
    /// </summary>
    [MaxLength(255)]
    public string? Title { get; set; }

    /// <summary>
    /// Full review text.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// JSON array of uploaded photo URLs (max 5).
    /// </summary>
    public string? PhotosJson { get; set; }

    /// <summary>
    /// Number of "helpful" votes from other users.
    /// </summary>
    public int HelpfulVotes { get; set; } = 0;

    /// <summary>
    /// Current moderation status.
    /// </summary>
    public ReviewStatus Status { get; set; } = ReviewStatus.Pending;

    /// <summary>
    /// Internal admin notes for moderation.
    /// </summary>
    public string? ModerationNotes { get; set; }

    /// <summary>
    /// User ID of the moderator who reviewed.
    /// </summary>
    public long? ModeratedById { get; set; }

    /// <summary>
    /// Timestamp when the review was moderated.
    /// </summary>
    public DateTime? ModeratedAt { get; set; }

    /// <summary>
    /// Booking completion timestamp (proves genuine review).
    /// Null if review is not verified.
    /// </summary>
    public DateTime? VerifiedPurchase { get; set; }

    /// <summary>
    /// Foreign key to the associated booking (if verified).
    /// </summary>
    public long? BookingId { get; set; }

    /// <summary>
    /// Indicates if the review was auto-flagged by spam detection.
    /// </summary>
    public bool AutoFlagged { get; set; } = false;

    /// <summary>
    /// Reason for auto-flagging.
    /// </summary>
    [MaxLength(500)]
    public string? AutoFlagReason { get; set; }

    // Navigation Properties

    /// <summary>
    /// The user who wrote the review.
    /// </summary>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// The moderator who reviewed.
    /// </summary>
    public virtual User? ModeratedBy { get; set; }

    /// <summary>
    /// The associated booking.
    /// </summary>
    public virtual Booking? Booking { get; set; }

    // Helper Methods

    /// <summary>
    /// Checks if the review is visible to public.
    /// </summary>
    public bool IsVisible => Status == ReviewStatus.Approved;

    /// <summary>
    /// Checks if the review is a verified purchase.
    /// </summary>
    public bool IsVerified => VerifiedPurchase.HasValue;

    /// <summary>
    /// Approves the review.
    /// </summary>
    public void Approve(long moderatorId, string? notes = null)
    {
        Status = ReviewStatus.Approved;
        ModeratedById = moderatorId;
        ModeratedAt = DateTime.UtcNow;
        ModerationNotes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Rejects the review.
    /// </summary>
    public void Reject(long moderatorId, string reason)
    {
        Status = ReviewStatus.Rejected;
        ModeratedById = moderatorId;
        ModeratedAt = DateTime.UtcNow;
        ModerationNotes = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Flags the review for manual moderation.
    /// </summary>
    public void Flag(string reason, bool isAutomatic = false)
    {
        Status = ReviewStatus.Flagged;
        AutoFlagged = isAutomatic;
        AutoFlagReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }
}
