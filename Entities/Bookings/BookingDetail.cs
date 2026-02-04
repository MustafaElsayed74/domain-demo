using System.ComponentModel.DataAnnotations;
using TravelMarketplace.Api.Entities.Common;

namespace TravelMarketplace.Api.Entities.Bookings;

/// <summary>
/// Represents detailed information for a booking.
/// One-to-one relationship with Booking.
/// </summary>
public class BookingDetail : BaseEntity
{
    /// <summary>
    /// Foreign key to the booking (one-to-one).
    /// </summary>
    public long BookingId { get; set; }

    /// <summary>
    /// Passenger details for flight bookings.
    /// JSON array: [{"title": "Mr", "first_name": "John", "last_name": "Doe", "dob": "1990-01-01", "passport": "X1234567"}]
    /// </summary>
    public string? PassengerDetailsJson { get; set; }

    /// <summary>
    /// Seat selections for flight bookings.
    /// JSON array: [{"passenger_id": 1, "seat": "12A"}]
    /// </summary>
    public string? SeatSelectionsJson { get; set; }

    /// <summary>
    /// Participant details for tour bookings.
    /// JSON: {"adults": 2, "children": 1, "infants": 0, "names": [...]}
    /// </summary>
    public string? ParticipantDetailsJson { get; set; }

    /// <summary>
    /// Driver details for car rental bookings.
    /// JSON: {"name": "John Doe", "age": 30, "license_number": "DL123", "license_expiry": "2028-12-31"}
    /// </summary>
    public string? DriverDetailsJson { get; set; }

    /// <summary>
    /// Guest details for hotel bookings.
    /// JSON array: [{"name": "John Doe", "age": 35}, {"name": "Jane Doe", "age": 32}]
    /// </summary>
    public string? GuestDetailsJson { get; set; }

    /// <summary>
    /// Selected extras with quantities.
    /// JSON array: [{"extra_id": 5, "name": "GPS", "quantity": 1, "price": 10.00}]
    /// </summary>
    public string? ExtrasSelectedJson { get; set; }

    /// <summary>
    /// Detailed price breakdown snapshot at booking time.
    /// JSON object with itemized pricing.
    /// </summary>
    public string? PricingBreakdownJson { get; set; }

    /// <summary>
    /// Special requests (dietary restrictions, accessibility needs, etc.).
    /// JSON object.
    /// </summary>
    public string? SpecialRequestsJson { get; set; }

    /// <summary>
    /// For tours: number of adults.
    /// </summary>
    public int? Adults { get; set; }

    /// <summary>
    /// For tours: number of children.
    /// </summary>
    public int? Children { get; set; }

    /// <summary>
    /// For tours: number of infants.
    /// </summary>
    public int? Infants { get; set; }

    /// <summary>
    /// For tours: selected schedule ID.
    /// </summary>
    public long? TourScheduleId { get; set; }

    /// <summary>
    /// For tours: selected price tier ID.
    /// </summary>
    public long? TourPriceTierId { get; set; }

    /// <summary>
    /// For flights: selected fare ID.
    /// </summary>
    public long? FlightFareId { get; set; }

    /// <summary>
    /// For hotels: number of rooms booked.
    /// </summary>
    public int? RoomsBooked { get; set; }

    /// <summary>
    /// For cars: rental start date/time.
    /// </summary>
    public DateTime? PickupDateTime { get; set; }

    /// <summary>
    /// For cars: rental end date/time.
    /// </summary>
    public DateTime? DropoffDateTime { get; set; }

    /// <summary>
    /// For cars: pickup location ID.
    /// </summary>
    public long? PickupLocationId { get; set; }

    /// <summary>
    /// For cars: dropoff location ID (if different from pickup).
    /// </summary>
    public long? DropoffLocationId { get; set; }

    // Navigation Properties

    /// <summary>
    /// The parent booking.
    /// </summary>
    public virtual Booking Booking { get; set; } = null!;
}
