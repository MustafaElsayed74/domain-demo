using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Payments;

/// <summary>
/// Represents a payment transaction for a booking.
/// Handles multiple payment gateways with PCI compliance.
/// </summary>
public class Payment : BaseEntity
{
    /// <summary>
    /// Foreign key to the booking.
    /// </summary>
    public long BookingId { get; set; }

    /// <summary>
    /// Payment amount.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Payment gateway/processor used.
    /// </summary>
    [Required]
    public PaymentGateway Gateway { get; set; }

    /// <summary>
    /// Current payment status.
    /// Reuses PaymentStatus enum from bookings.
    /// </summary>
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    /// <summary>
    /// Unique transaction ID from the payment gateway.
    /// </summary>
    [MaxLength(255)]
    public string? TransactionId { get; set; }

    /// <summary>
    /// Full response from payment gateway (JSON).
    /// Stored for debugging and reconciliation.
    /// </summary>
    public string? GatewayResponseJson { get; set; }

    /// <summary>
    /// Payment method type (card, wallet, bank_transfer).
    /// </summary>
    [MaxLength(50)]
    public string? PaymentMethod { get; set; }

    /// <summary>
    /// Last 4 digits of the card (PCI compliant - never store full PAN).
    /// </summary>
    [MaxLength(4)]
    public string? CardLast4 { get; set; }

    /// <summary>
    /// Card brand (Visa, Mastercard, Amex, etc.).
    /// </summary>
    [MaxLength(50)]
    public string? CardBrand { get; set; }

    /// <summary>
    /// Tokenized card reference for recurring payments.
    /// </summary>
    [MaxLength(255)]
    public string? PaymentToken { get; set; }

    /// <summary>
    /// Amount refunded (for partial/full refunds).
    /// Must be less than or equal to Amount.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? RefundAmount { get; set; }

    /// <summary>
    /// Refund transaction ID from the gateway.
    /// </summary>
    [MaxLength(255)]
    public string? RefundTransactionId { get; set; }

    /// <summary>
    /// Timestamp when payment was successfully completed.
    /// </summary>
    public DateTime? PaidAt { get; set; }

    /// <summary>
    /// Timestamp when refund was completed.
    /// </summary>
    public DateTime? RefundedAt { get; set; }

    /// <summary>
    /// Number of retry attempts for failed payments.
    /// </summary>
    public int RetryCount { get; set; } = 0;

    /// <summary>
    /// Error message from the gateway (if failed).
    /// </summary>
    [MaxLength(500)]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Error code from the gateway.
    /// </summary>
    [MaxLength(100)]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// User ID who initiated the payment.
    /// </summary>
    public long? InitiatedById { get; set; }

    /// <summary>
    /// User ID who processed the refund.
    /// </summary>
    public long? RefundedById { get; set; }

    /// <summary>
    /// IP address of the payer (for fraud detection).
    /// </summary>
    [MaxLength(50)]
    public string? PayerIpAddress { get; set; }

    /// <summary>
    /// Billing address JSON (minimal for PCI compliance).
    /// </summary>
    public string? BillingAddressJson { get; set; }

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent booking.
    /// </summary>
    public virtual Booking Booking { get; set; } = null!;

    /// <summary>
    /// User who initiated the payment.
    /// </summary>
    public virtual User? InitiatedBy { get; set; }

    /// <summary>
    /// User who processed the refund.
    /// </summary>
    public virtual User? RefundedBy { get; set; }

    // Helper Methods

    /// <summary>
    /// Checks if payment can be refunded.
    /// </summary>
    public bool CanRefund => Status == PaymentStatus.Paid && 
                             (!RefundAmount.HasValue || RefundAmount < Amount);

    /// <summary>
    /// Gets remaining refundable amount.
    /// </summary>
    public decimal RefundableAmount => Amount - (RefundAmount ?? 0);

    /// <summary>
    /// Marks payment as completed.
    /// </summary>
    public void MarkAsPaid(string transactionId)
    {
        Status = PaymentStatus.Paid;
        TransactionId = transactionId;
        PaidAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks payment as failed.
    /// </summary>
    public void MarkAsFailed(string errorCode, string errorMessage)
    {
        Status = PaymentStatus.Failed;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        RetryCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Processes a refund.
    /// </summary>
    public bool ProcessRefund(decimal amount, string refundTransactionId, long? refundedById = null)
    {
        if (amount > RefundableAmount)
            return false;

        RefundAmount = (RefundAmount ?? 0) + amount;
        RefundTransactionId = refundTransactionId;
        RefundedAt = DateTime.UtcNow;
        RefundedById = refundedById;

        if (RefundAmount >= Amount)
            Status = PaymentStatus.Refunded;

        UpdatedAt = DateTime.UtcNow;
        return true;
    }
}
