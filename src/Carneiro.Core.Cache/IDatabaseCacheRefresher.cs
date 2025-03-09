namespace Carneiro.Core.Cache;

/// <summary>
/// The database cache refresher interface.
/// </summary>
public interface IDatabaseCacheRefresher
{
    /// <summary>
    /// Gets the name of the database cache refresher.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Refreshes all cache.
    /// </summary>
    Task RefreshAllCacheAsync();
}

/// <summary>
/// The database cache refresher interface.
/// </summary>
public interface IDatabaseCacheRefresher<TDbContext> : IDatabaseCacheRefresher
    where TDbContext : DbContext
{
}