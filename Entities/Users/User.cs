using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Users;

/// <summary>
/// Represents a user in the travel marketplace platform.
/// Supports both email/password and social authentication.
/// </summary>
public class User : SoftDeletableEntity
{
    /// <summary>
    /// User's full name.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// User's email address. Must be unique and is used for login.
    /// Indexed for fast lookups.
    /// </summary>
    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Bcrypt/Argon2 hashed password.
    /// Nullable for social-only accounts (users who only use OAuth).
    /// Minimum 8 characters with uppercase, lowercase, number, and special character required.
    /// </summary>
    [MaxLength(255)]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Contact phone number with international country code (e.g., +1234567890).
    /// </summary>
    [MaxLength(50)]
    [Phone]
    public string? Phone { get; set; }

    /// <summary>
    /// User's city/country location.
    /// </summary>
    [MaxLength(255)]
    public string? Location { get; set; }

    /// <summary>
    /// URL to user's profile picture (S3/CDN URL).
    /// </summary>
    [MaxLength(500)]
    [Url]
    public string? ProfilePicture { get; set; }

    /// <summary>
    /// Indicates whether the user's email has been verified.
    /// </summary>
    public bool IsVerified { get; set; } = false;

    /// <summary>
    /// Current status of the user account.
    /// </summary>
    public UserStatus Status { get; set; } = UserStatus.Active;

    /// <summary>
    /// Number of consecutive failed login attempts.
    /// Used for account lockout after 3 failed attempts.
    /// </summary>
    public int FailedLoginAttempts { get; set; } = 0;

    /// <summary>
    /// Timestamp when the account lockout expires.
    /// Null if account is not locked.
    /// </summary>
    public DateTime? LockoutEndTime { get; set; }

    /// <summary>
    /// Token for "remember me" functionality (30-day refresh token).
    /// </summary>
    [MaxLength(500)]
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Expiry time for the refresh token.
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation Properties

    /// <summary>
    /// Collection of OTP codes associated with this user.
    /// </summary>
    public virtual ICollection<OtpCode> OtpCodes { get; set; } = new List<OtpCode>();

    /// <summary>
    /// Collection of social identity providers linked to this user.
    /// </summary>
    public virtual ICollection<SocialIdentity> SocialIdentities { get; set; } = new List<SocialIdentity>();
}
