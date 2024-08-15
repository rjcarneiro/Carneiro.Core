namespace Carneiro.Core.Entities;

/// <summary>
/// Default implementation for <see cref="IAuditableEntity"/>.
/// </summary>
/// <seealso cref="IAuditableEntity" />
public abstract class AuditableEntity : IAuditableEntity
{
    /// <inheritdoc />
    public virtual bool IsActive { get; set; }

    /// <inheritdoc />
    public virtual DateTime CreateDate { get; set; }

    /// <inheritdoc />
    public virtual DateTime? UpdateDate { get; set; }

    /// <inheritdoc />
    public virtual DateTime? DeleteDate { get; set; }

    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }
}