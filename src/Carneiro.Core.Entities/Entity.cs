namespace Carneiro.Core.Entities;

/// <summary>
/// Default implementation for <see cref="IEntity"/>.
/// </summary>
public abstract class Entity : AuditableEntity, IEntity
{
    /// <inheritdoc />
    public virtual int Id { get; set; }
}