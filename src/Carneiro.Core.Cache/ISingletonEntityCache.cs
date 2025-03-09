namespace Carneiro.Core.Cache;

/// <summary>
/// The singleton entity cache interface.
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public interface ISingletonEntityCache<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// Initializes the cache.
    /// </summary>
    /// <param name="db"></param>
    Task InitializeAsync(IUnitOfWork<TDbContext> db);

    /// <summary>
    /// Refreshes the cache.
    /// </summary>
    /// <param name="db"></param>
    Task RefreshAsync(IUnitOfWork<TDbContext> db);

    /// <summary>
    /// Gets the refresh period. Defaults a <c>5 minutes</c>.
    /// </summary>
    TimeSpan GetRefreshPeriod { get; }
}