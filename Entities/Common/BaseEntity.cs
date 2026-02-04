namespace TravelMarketplace.Api.Entities.Common;

/// <summary>
/// Base entity class providing common audit fields for all entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primary key identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Timestamp when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the entity was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
