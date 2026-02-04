using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Users;

/// <summary>
/// Represents a one-time password code for user verification, password reset, or 2FA.
/// OTPs expire after 10 minutes and can only be used once.
/// </summary>
public class OtpCode : BaseEntity
{
    /// <summary>
    /// Foreign key to the user this OTP belongs to.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// The 4-6 digit OTP code.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// The purpose of this OTP (verification, reset, 2FA).
    /// </summary>
    [Required]
    public OtpPurpose Purpose { get; set; }

    /// <summary>
    /// Timestamp when this OTP expires (10 minutes from creation).
    /// </summary>
    [Required]
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Indicates whether this OTP has been used.
    /// OTPs are single-use only.
    /// </summary>
    public bool Used { get; set; } = false;

    /// <summary>
    /// Number of failed verification attempts for this OTP.
    /// Account is locked for 15 minutes after 3 failed attempts.
    /// </summary>
    public int FailedAttempts { get; set; } = 0;

    // Navigation Properties

    /// <summary>
    /// The user this OTP belongs to.
    /// </summary>
    public virtual User User { get; set; } = null!;

    // Helper Methods

    /// <summary>
    /// Checks if the OTP is still valid (not expired, not used, not exceeded attempts).
    /// </summary>
    public bool IsValid => !Used && ExpiresAt > DateTime.UtcNow && FailedAttempts < 3;

    /// <summary>
    /// Creates a new OTP code with 10-minute expiry.
    /// </summary>
    public static OtpCode Create(long userId, OtpPurpose purpose, string code)
    {
        return new OtpCode
        {
            UserId = userId,
            Code = code,
            Purpose = purpose,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
