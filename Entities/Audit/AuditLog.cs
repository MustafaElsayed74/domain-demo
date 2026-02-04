using System.ComponentModel.DataAnnotations;

namespace TravelMarketplace.Api.Entities.Audit;

/// <summary>
/// Represents an immutable audit log entry for administrative actions.
/// INSERT only - no UPDATE or DELETE allowed.
/// Retention: 7 years for compliance.
/// </summary>
public class AuditLog
{
    /// <summary>
    /// Primary key identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// User who performed the action. Null for system actions.
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// Action performed (e.g., "CREATE_TOUR", "UPDATE_BOOKING", "DELETE_USER").
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Type of entity affected (e.g., "tours", "bookings", "users").
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string EntityType { get; set; } = string.Empty;

    /// <summary>
    /// ID of the affected entity. Null for bulk operations.
    /// </summary>
    public long? EntityId { get; set; }

    /// <summary>
    /// JSON snapshot of entity state before the action.
    /// Sensitive data should be encrypted.
    /// </summary>
    public string? OldValuesJson { get; set; }

    /// <summary>
    /// JSON snapshot of entity state after the action.
    /// Sensitive data should be encrypted.
    /// </summary>
    public string? NewValuesJson { get; set; }

    /// <summary>
    /// IP address of the user (IPv4 or IPv6).
    /// </summary>
    [MaxLength(45)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Browser/device user agent string.
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// HTTP method used (GET, POST, PUT, DELETE).
    /// </summary>
    [MaxLength(10)]
    public string? HttpMethod { get; set; }

    /// <summary>
    /// Request URL/endpoint.
    /// </summary>
    [MaxLength(500)]
    public string? RequestUrl { get; set; }

    /// <summary>
    /// HTTP response status code.
    /// </summary>
    public int? ResponseStatusCode { get; set; }

    /// <summary>
    /// Duration of the action in milliseconds.
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// Indicates if the action was successful.
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// Error message if the action failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Additional context/metadata as JSON.
    /// </summary>
    public string? ContextJson { get; set; }

    /// <summary>
    /// Timestamp when the action occurred.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    /// <summary>
    /// The user who performed the action.
    /// </summary>
    public virtual User? User { get; set; }

    // Static Factory Methods

    /// <summary>
    /// Creates an audit log for entity creation.
    /// </summary>
    public static AuditLog Create(
        string entityType,
        long entityId,
        object newValues,
        long? userId = null,
        string? ipAddress = null,
        string? userAgent = null)
    {
        return new AuditLog
        {
            UserId = userId,
            Action = $"CREATE_{entityType.ToUpperInvariant()}",
            EntityType = entityType.ToLowerInvariant(),
            EntityId = entityId,
            NewValuesJson = System.Text.Json.JsonSerializer.Serialize(newValues),
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates an audit log for entity update.
    /// </summary>
    public static AuditLog Update(
        string entityType,
        long entityId,
        object oldValues,
        object newValues,
        long? userId = null,
        string? ipAddress = null,
        string? userAgent = null)
    {
        return new AuditLog
        {
            UserId = userId,
            Action = $"UPDATE_{entityType.ToUpperInvariant()}",
            EntityType = entityType.ToLowerInvariant(),
            EntityId = entityId,
            OldValuesJson = System.Text.Json.JsonSerializer.Serialize(oldValues),
            NewValuesJson = System.Text.Json.JsonSerializer.Serialize(newValues),
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates an audit log for entity deletion.
    /// </summary>
    public static AuditLog Delete(
        string entityType,
        long entityId,
        object oldValues,
        long? userId = null,
        string? ipAddress = null,
        string? userAgent = null)
    {
        return new AuditLog
        {
            UserId = userId,
            Action = $"DELETE_{entityType.ToUpperInvariant()}",
            EntityType = entityType.ToLowerInvariant(),
            EntityId = entityId,
            OldValuesJson = System.Text.Json.JsonSerializer.Serialize(oldValues),
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates an audit log for a custom action.
    /// </summary>
    public static AuditLog Custom(
        string action,
        string entityType,
        long? entityId = null,
        object? context = null,
        long? userId = null,
        string? ipAddress = null,
        string? userAgent = null)
    {
        return new AuditLog
        {
            UserId = userId,
            Action = action.ToUpperInvariant(),
            EntityType = entityType.ToLowerInvariant(),
            EntityId = entityId,
            ContextJson = context != null ? System.Text.Json.JsonSerializer.Serialize(context) : null,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow
        };
    }
}
