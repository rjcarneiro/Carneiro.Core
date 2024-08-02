namespace Carneiro.Core.Entities;

/// <summary>
/// Default implementation for <see cref="IAuditableDistributedEntity"/>.
/// </summary>
/// <seealso cref="IAuditableEntity" />
public abstract class AuditableDistributedEntity : EntityDistributed, IAuditableDistributedEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    /// <value>
    /// The create date.
    /// </value>
    public virtual DateTime CreateDate { get; set; }

    /// <summary>
    /// Gets or sets the update date.
    /// </summary>
    /// <value>
    /// The update date.
    /// </value>
    public virtual DateTime? UpdateDate { get; set; }

    /// <summary>
    /// Gets or sets the delete date.
    /// </summary>
    /// <value>
    /// The delete date.
    /// </value>
    public virtual DateTime? DeleteDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is deleted.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsDeleted { get; set; }
}