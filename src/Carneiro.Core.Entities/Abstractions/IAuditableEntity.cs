namespace Carneiro.Core.Entities.Abstractions;

/// <summary>
/// Interface for a default auditable database entity.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    DateTime CreateDate { get; set; }

    /// <summary>
    /// Gets or sets the update date.
    /// </summary>
    DateTime? UpdateDate { get; set; }

    /// <summary>
    /// Gets or sets the delete date.
    /// </summary>
    DateTime? DeleteDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}