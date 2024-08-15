namespace Carneiro.Core.Entities.Abstractions;

/// <summary>
/// Interface for a default database entity. Defaults as <see cref="int"/> type.
/// </summary>
public interface IEntity : IAuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    int Id { get; set; }
}