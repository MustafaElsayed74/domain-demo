using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Users;

/// <summary>
/// Represents a social identity provider linked to a user account.
/// Supports OAuth 2.0 providers like Google, Facebook, and Apple.
/// </summary>
public class SocialIdentity : BaseEntity
{
    /// <summary>
    /// Foreign key to the user this social identity belongs to.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// The OAuth provider name (e.g., 'google', 'facebook', 'apple').
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// The unique user ID from the OAuth provider (e.g., Google's 'sub' claim).
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string ProviderUserId { get; set; } = string.Empty;

    /// <summary>
    /// Email address from the OAuth provider.
    /// May differ from the user's primary email.
    /// </summary>
    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Raw profile data from the OAuth provider stored as JSON.
    /// Contains minimal PII, prefer tokens over storing sensitive data.
    /// </summary>
    public string? ProfileJson { get; set; }

    /// <summary>
    /// Access token from the OAuth provider (encrypted at rest).
    /// Used for accessing provider APIs on behalf of the user.
    /// </summary>
    [MaxLength(2000)]
    public string? AccessToken { get; set; }

    /// <summary>
    /// Expiry time for the access token.
    /// </summary>
    public DateTime? AccessTokenExpiresAt { get; set; }

    /// <summary>
    /// Refresh token from the OAuth provider (encrypted at rest).
    /// Used to obtain new access tokens without re-authentication.
    /// </summary>
    [MaxLength(2000)]
    public string? RefreshToken { get; set; }

    // Navigation Properties

    /// <summary>
    /// The user this social identity belongs to.
    /// </summary>
    public virtual User User { get; set; } = null!;
}
