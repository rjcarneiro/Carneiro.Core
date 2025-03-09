namespace Carneiro.Core.Cache;

/// <summary>
/// The entity cache interface.
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TCachedEntity"></typeparam>
public interface IEntityCache<TDbContext, TKey, TCachedEntity> : ISingletonEntityCache<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// Gets the cache dictionary.
    /// </summary>
    Dictionary<TKey, TCachedEntity> GetDictionary();
}