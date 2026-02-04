namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the purpose of an OTP code.
/// </summary>
public enum OtpPurpose
{
    /// <summary>
    /// OTP for email verification during registration.
    /// </summary>
    Verify = 0,

    /// <summary>
    /// OTP for password reset flow.
    /// </summary>
    Reset = 1,

    /// <summary>
    /// OTP for two-factor authentication.
    /// </summary>
    TwoFactorAuth = 2
}
