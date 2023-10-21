namespace Carneiro.Core.Entities;

/// <summary>
/// Default implementation for <see cref="IEntity"/>.
/// </summary>
public abstract class Entity : IEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public virtual int Id { get; set; }
}