namespace Carneiro.Core.Entities;

/// <summary>
/// Default implementation for <see cref="IEntityDistributed"/>.
/// </summary>
public abstract class EntityDistributed : AuditableEntity, IEntityDistributed
{
    /// <inheritdoc />
    public virtual Guid Id { get; set; } = Guid.NewGuid();
}