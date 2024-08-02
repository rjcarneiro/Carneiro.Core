namespace Carneiro.Core.Entities.Abstractions;

/// <summary>
/// Interface for a default database entity.
/// </summary>
public interface IEntityDistributed
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    Guid Id { get; set; }
}