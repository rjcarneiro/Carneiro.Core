namespace Carneiro.Core.Exceptions;

/// <summary>
/// The exception happens when an entity is not found.
/// </summary>
/// <seealso cref="Carneiro.Core.Exceptions.RzException" />
public class EntityNotFoundRzException : RzException
{
    /// <summary>
    /// Gets the entity.
    /// </summary>
    /// <value>
    /// The entity.
    /// </value>
    public string Entity { get; }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public string Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundRzException"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public EntityNotFoundRzException(string entity)
        : base($"The entity '{entity}' was not found.")
    {
        Entity = entity;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundRzException"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="id">The identifier.</param>
    public EntityNotFoundRzException(string entity, int id)
        : this(entity, id.ToString())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundRzException"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="id">The identifier.</param>
    public EntityNotFoundRzException(string entity, string id)
        : base($"The entity '{entity}' with id '{id}' was not found.")
    {
        Entity = entity;
        Id = id;
    }
}