namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the moderation status of a review.
/// </summary>
public enum ReviewStatus
{
    /// <summary>
    /// Review is pending moderation.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Review has been approved and is visible.
    /// </summary>
    Approved = 1,

    /// <summary>
    /// Review has been rejected by moderator.
    /// </summary>
    Rejected = 2,

    /// <summary>
    /// Review has been flagged for additional review.
    /// </summary>
    Flagged = 3
}
