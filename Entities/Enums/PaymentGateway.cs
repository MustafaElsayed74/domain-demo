namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the payment gateway/processor used.
/// </summary>
public enum PaymentGateway
{
    /// <summary>
    /// Stripe payment processor.
    /// </summary>
    Stripe = 0,

    /// <summary>
    /// PayPal payment processor.
    /// </summary>
    PayPal = 1,

    /// <summary>
    /// Local/bank payment processor.
    /// </summary>
    Local = 2
}
