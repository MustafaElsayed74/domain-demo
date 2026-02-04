namespace TravelMarketplace.Api.Entities.Enums;

/// <summary>
/// Represents the fuel type of a vehicle.
/// </summary>
public enum FuelType
{
    /// <summary>
    /// Petrol/gasoline engine.
    /// </summary>
    Petrol = 0,

    /// <summary>
    /// Diesel engine.
    /// </summary>
    Diesel = 1,

    /// <summary>
    /// Fully electric vehicle.
    /// </summary>
    Electric = 2,

    /// <summary>
    /// Hybrid (petrol + electric).
    /// </summary>
    Hybrid = 3
}
