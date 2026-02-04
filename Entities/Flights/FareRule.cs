using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Flights;

/// <summary>
/// Represents fare rules for flight tickets.
/// Defines cancellation and change policies.
/// </summary>
public class FareRule : BaseEntity
{
    /// <summary>
    /// Rule name (e.g., "Flexible", "Economy Saver").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the fare rules.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// JSON object with cancellation policy terms.
    /// </summary>
    public string? CancellationPolicyJson { get; set; }

    /// <summary>
    /// JSON object with change/rebooking policy terms.
    /// </summary>
    public string? ChangePolicyJson { get; set; }

    /// <summary>
    /// Fee charged for ticket cancellation.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? CancellationFee { get; set; }

    /// <summary>
    /// Fee charged for ticket changes.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? ChangeFee { get; set; }

    /// <summary>
    /// Indicates if this fare rule is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation Properties

    /// <summary>
    /// Collection of flight fares using this rule.
    /// </summary>
    public virtual ICollection<FlightFare> FlightFares { get; set; } = new List<FlightFare>();
}
