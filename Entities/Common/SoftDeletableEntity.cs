using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMarketplace.Api.Entities.Common;

/// <summary>
/// Base entity class for entities that support GDPR-compliant soft delete.
/// Inherits from BaseEntity and adds DeletedAt for soft delete tracking.
/// </summary>
public abstract class SoftDeletableEntity : BaseEntity
{
    /// <summary>
    /// Timestamp when the entity was soft deleted. Null if active.
    /// Data should be hard deleted 30 days after this timestamp for GDPR compliance.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Indicates whether the entity has been soft deleted.
    /// </summary>
    [NotMapped]
    public bool IsDeleted => DeletedAt.HasValue;
}

