namespace Carneiro.Core.Entities.Abstractions;

/// <summary>
/// Interface for a default auditable database entity.
/// </summary>
public interface IAuditableDistributedEntity : IEntityDistributed
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the create date.
    /// </summary>
    /// <value>
    /// The create date.
    /// </value>
    DateTime CreateDate { get; set; }

    /// <summary>
    /// Gets or sets the update date.
    /// </summary>
    /// <value>
    /// The update date.
    /// </value>
    DateTime? UpdateDate { get; set; }

    /// <summary>
    /// Gets or sets the delete date.
    /// </summary>
    /// <value>
    /// The delete date.
    /// </value>
    DateTime? DeleteDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is deleted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
    /// </value>
    bool IsDeleted { get; set; }
}