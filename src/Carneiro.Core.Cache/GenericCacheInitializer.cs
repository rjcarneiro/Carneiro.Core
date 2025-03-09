namespace Carneiro.Core.Cache;

/// <summary>
/// The generic cache initializer.
/// </summary>
/// <param name="singletonEntityCaches"></param>
/// <param name="serviceProvider"></param>
/// <typeparam name="TDbContext"></typeparam>
public class GenericCacheInitializer<TDbContext>(IEnumerable<ISingletonEntityCache<TDbContext>> singletonEntityCaches, IServiceProvider serviceProvider) : IAsyncInitializer
    where TDbContext : DbContext
{
    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        await using AsyncServiceScope scoped = serviceProvider.CreateAsyncScope();
        IUnitOfWork<TDbContext> db = scoped.ServiceProvider.GetRequiredService<IUnitOfWork<TDbContext>>();

        foreach (ISingletonEntityCache<TDbContext> cache in singletonEntityCaches)
        {
            await cache.InitializeAsync(db);
        }
    }
}