namespace Carneiro.Core.Entities.Abstractions;

/// <summary>
/// Interface for a default database entity.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    int Id { get; set; }
}