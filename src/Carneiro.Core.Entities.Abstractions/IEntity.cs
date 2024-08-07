namespace Carneiro.Core.Entities.Abstractions;

/// <summary>
/// Interface for a default database entity. Defaults as <see cref="int"/> type.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }
}