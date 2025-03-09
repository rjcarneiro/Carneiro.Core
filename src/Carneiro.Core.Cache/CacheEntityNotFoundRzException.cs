namespace Carneiro.Core.Cache;

/// <summary>
/// The exception is thrown if a certain entity cannot be found.
/// </summary>
public class CacheEntityNotFoundRzException : CacheRzException
{
    /// <summary>
    /// Gets the entity name.
    /// </summary>
    public string Entity { get; }

    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    public string EntityId { get; }

    /// <inheritdoc />
    public CacheEntityNotFoundRzException(string entity)
        : base($"Entity {entity} was not found")
    {
        Entity = entity;
    }

    /// <inheritdoc />
    public CacheEntityNotFoundRzException(string entity, long id)
        : this(entity, id.ToString())
    {
    }

    /// <inheritdoc />
    public CacheEntityNotFoundRzException(string entity, string id)
        : base($"Entity {entity} with id '{id}' was not found")
    {
        Entity = entity;
        EntityId = id;
    }
}