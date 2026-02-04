using Microsoft.EntityFrameworkCore;
using TravelMarketplace.Api.Entities.Audit;
using TravelMarketplace.Api.Entities.Bookings;
using TravelMarketplace.Api.Entities.Cars;
using TravelMarketplace.Api.Entities.Core;
using TravelMarketplace.Api.Entities.Coupons;
using TravelMarketplace.Api.Entities.Flights;
using TravelMarketplace.Api.Entities.Hotels;
using TravelMarketplace.Api.Entities.Payments;
using TravelMarketplace.Api.Entities.Reviews;
using TravelMarketplace.Api.Entities.Tours;
using TravelMarketplace.Api.Entities.Users;

namespace TravelMarketplace.Api.Data;

/// <summary>
/// Entity Framework Core DbContext for Travel Marketplace.
/// </summary>
public class TravelMarketplaceDbContext : DbContext
{
    public TravelMarketplaceDbContext(DbContextOptions<TravelMarketplaceDbContext> options)
        : base(options)
    {
    }

    // User Management
    public DbSet<User> Users => Set<User>();
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();
    public DbSet<SocialIdentity> SocialIdentities => Set<SocialIdentity>();

    // Core
    public DbSet<Category> Categories => Set<Category>();

    // Tours
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<TourSchedule> TourSchedules => Set<TourSchedule>();
    public DbSet<TourPriceTier> TourPriceTiers => Set<TourPriceTier>();
    public DbSet<TourImage> TourImages => Set<TourImage>();

    // Flights
    public DbSet<Carrier> Carriers => Set<Carrier>();
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<FareRule> FareRules => Set<FareRule>();
    public DbSet<FlightFare> FlightFares => Set<FlightFare>();
    public DbSet<FlightSeat> FlightSeats => Set<FlightSeat>();

    // Cars
    public DbSet<CarBrand> CarBrands => Set<CarBrand>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<CarPricingTier> CarPricingTiers => Set<CarPricingTier>();
    public DbSet<CarImage> CarImages => Set<CarImage>();
    public DbSet<CarExtra> CarExtras => Set<CarExtra>();

    // Hotels
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RoomImage> RoomImages => Set<RoomImage>();

    // Bookings
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingDetail> BookingDetails => Set<BookingDetail>();
    public DbSet<BookingStatusHistory> BookingStatusHistories => Set<BookingStatusHistory>();

    // Payments
    public DbSet<Payment> Payments => Set<Payment>();

    // Reviews
    public DbSet<Review> Reviews => Set<Review>();

    // Favorites
    public DbSet<Favorite> Favorites => Set<Favorite>();

    // Coupons
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<CouponUsage> CouponUsages => Set<CouponUsage>();

    // Audit
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set default delete behavior to Restrict to avoid SQL Server cascade cycle issues
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelMarketplaceDbContext).Assembly);
    }
}
