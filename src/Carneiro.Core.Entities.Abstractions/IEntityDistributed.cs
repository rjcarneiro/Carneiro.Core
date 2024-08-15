namespace Carneiro.Core.Entities.Abstractions;

/// <summary>
/// Interface for a default database entity. Defaults as <see cref="Guid"/> type.
/// </summary>
public interface IEntityDistributed : IAuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    Guid Id { get; set; }
}