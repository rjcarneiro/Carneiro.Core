namespace Carneiro.Core.Entities;

/// <summary>
/// Default implementation for <see cref="IEntityDistributed"/>.
/// </summary>
public abstract class EntityDistributed : IEntityDistributed
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public virtual Guid Id { get; set; } = Guid.NewGuid();
}