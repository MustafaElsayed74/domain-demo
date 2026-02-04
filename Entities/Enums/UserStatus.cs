namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the status of a user account.
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// User account is active and can access all features.
    /// </summary>
    Active = 0,

    /// <summary>
    /// User account is inactive (e.g., dormant, not yet activated).
    /// </summary>
    Inactive = 1,

    /// <summary>
    /// User account is suspended due to policy violation or admin action.
    /// </summary>
    Suspended = 2
}
