using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMarketplace.Api.Entities.Common;
using TravelMarketplace.Api.Entities.Enums;

namespace TravelMarketplace.Api.Entities.Flights;

/// <summary>
/// Represents a scheduled flight between two airports.
/// </summary>
public class Flight : BaseEntity
{
    /// <summary>
    /// Unique flight number (e.g., "EK215", "DL404").
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string FlightNumber { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key to the operating carrier/airline.
    /// </summary>
    public long CarrierId { get; set; }

    /// <summary>
    /// Aircraft type (e.g., "Boeing 777", "Airbus A380").
    /// </summary>
    [MaxLength(100)]
    public string? AircraftType { get; set; }

    /// <summary>
    /// Foreign key to the origin/departure airport.
    /// </summary>
    public long OriginAirportId { get; set; }

    /// <summary>
    /// Foreign key to the destination/arrival airport.
    /// </summary>
    public long DestinationAirportId { get; set; }

    /// <summary>
    /// Scheduled departure time (UTC).
    /// </summary>
    [Required]
    public DateTime DepartureAt { get; set; }

    /// <summary>
    /// Scheduled arrival time (UTC).
    /// Must be after DepartureAt.
    /// </summary>
    [Required]
    public DateTime ArrivalAt { get; set; }

    /// <summary>
    /// Flight duration in minutes.
    /// Calculated as (ArrivalAt - DepartureAt).
    /// </summary>
    [Required]
    public int DurationMinutes { get; set; }

    /// <summary>
    /// JSON array of stopover airports.
    /// Example: [{"airport": "DXB", "duration": 120}]
    /// </summary>
    public string? StopsJson { get; set; }

    /// <summary>
    /// JSON array of layover durations at each stop.
    /// </summary>
    public string? LayoverDurationsJson { get; set; }

    /// <summary>
    /// JSON object with baggage allowance rules.
    /// Example: {"checked": "2x23kg", "cabin": "1x7kg"}
    /// </summary>
    public string? BaggageRulesJson { get; set; }

    /// <summary>
    /// Indicates if the ticket is refundable.
    /// </summary>
    public bool Refundable { get; set; } = false;

    /// <summary>
    /// Current flight status.
    /// Auto-updated based on scheduled jobs.
    /// </summary>
    public FlightStatus Status { get; set; } = FlightStatus.Scheduled;

    /// <summary>
    /// Calculated average rating (0.00-5.00).
    /// </summary>
    [Column(TypeName = "decimal(3,2)")]
    public decimal? AvgRating { get; set; }

    /// <summary>
    /// Total number of reviews.
    /// </summary>
    public int ReviewCount { get; set; } = 0;

    /// <summary>
    /// User ID who created this flight record.
    /// </summary>
    public long? CreatedById { get; set; }

    /// <summary>
    /// User ID who last updated this flight record.
    /// </summary>
    public long? UpdatedById { get; set; }

    /// <summary>
    /// Row version for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Navigation Properties

    /// <summary>
    /// The operating carrier/airline.
    /// </summary>
    public virtual Carrier Carrier { get; set; } = null!;

    /// <summary>
    /// The origin/departure airport.
    /// </summary>
    public virtual Airport OriginAirport { get; set; } = null!;

    /// <summary>
    /// The destination/arrival airport.
    /// </summary>
    public virtual Airport DestinationAirport { get; set; } = null!;

    /// <summary>
    /// User who created this flight.
    /// </summary>
    public virtual User? CreatedBy { get; set; }

    /// <summary>
    /// User who last updated this flight.
    /// </summary>
    public virtual User? UpdatedBy { get; set; }

    /// <summary>
    /// Collection of fares for this flight.
    /// </summary>
    public virtual ICollection<FlightFare> Fares { get; set; } = new List<FlightFare>();

    /// <summary>
    /// Collection of seats for this flight.
    /// </summary>
    public virtual ICollection<FlightSeat> Seats { get; set; } = new List<FlightSeat>();

    // Business Logic

    /// <summary>
    /// Validates that arrival is after departure and duration matches.
    /// </summary>
    public bool IsValid => ArrivalAt > DepartureAt && 
                           DurationMinutes == (int)(ArrivalAt - DepartureAt).TotalMinutes;

    /// <summary>
    /// Number of stops (0 = direct flight).
    /// </summary>
    public int StopCount => string.IsNullOrEmpty(StopsJson) ? 0 : 
        System.Text.Json.JsonSerializer.Deserialize<object[]>(StopsJson)?.Length ?? 0;
}
