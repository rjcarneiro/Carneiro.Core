namespace Carneiro.Core.Cache;

/// <summary>
/// 
/// </summary>
public class CacheEntityNotFoundPosGatewayException : RzException
{
    /// <summary>
    /// 
    /// </summary>
    public string Entity { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string EntityId { get; init; }

    /// <inheritdoc />
    public CacheEntityNotFoundPosGatewayException(string entity)
        : base($"Entity {entity} was not found")
    {
        Entity = entity;
    }

    /// <inheritdoc />
    public CacheEntityNotFoundPosGatewayException(string entity, long id)
        : this(entity, id.ToString())
    {
    }

    /// <inheritdoc />
    public CacheEntityNotFoundPosGatewayException(string entity, string id)
        : base($"Entity {entity} with id '{id}' was not found")
    {
        Entity = entity;
        EntityId = id;
    }
}