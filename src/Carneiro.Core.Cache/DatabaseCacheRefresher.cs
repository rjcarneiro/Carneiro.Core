namespace Carneiro.Core.Cache;

/// <summary>
/// The database cache refresher implementation.
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public abstract class DatabaseCacheRefresher<TDbContext> : IDatabaseCacheRefresher<TDbContext>
    where TDbContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseCacheRefresher{TDbContext}"/> class.
    /// </summary>
    /// <param name="serviceProvider"></param>
    protected DatabaseCacheRefresher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public abstract string Name { get; }

    /// <inheritdoc />
    public async Task RefreshAllCacheAsync()
    {
        await using AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();

        IUnitOfWork<TDbContext> db = scope.ServiceProvider.GetRequiredService<IUnitOfWork<TDbContext>>();
        IEnumerable<ISingletonEntityCache<TDbContext>> singletonEntityCaches = scope.ServiceProvider.GetServices<ISingletonEntityCache<TDbContext>>();

        foreach (ISingletonEntityCache<TDbContext> cache in singletonEntityCaches)
        {
            await cache.RefreshAsync(db);
        }
    }
}