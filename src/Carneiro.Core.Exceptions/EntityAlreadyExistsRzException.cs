namespace Carneiro.Core.Exceptions;

/// <summary>
/// The exception happens when an entity is not found.
/// </summary>
/// <seealso cref="RzException" />
public class EntityAlreadyExistsRzException : RzException
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
    /// Initializes a new instance of the <see cref="EntityAlreadyExistsRzException"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public EntityAlreadyExistsRzException(string entity)
        : base($"The entity '{entity}' already exists.")
    {
        Entity = entity;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityAlreadyExistsRzException"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="id">The identifier.</param>
    public EntityAlreadyExistsRzException(string entity, int id)
        : this(entity, id.ToString())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityAlreadyExistsRzException"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="id">The identifier.</param>
    public EntityAlreadyExistsRzException(string entity, string id)
        : base($"The entity '{entity}' with id '{id}' already exists.")
    {
        Entity = entity;
        Id = id;
    }
}