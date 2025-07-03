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
        await using var scoped = serviceProvider.CreateAsyncScope();
        var db = scoped.ServiceProvider.GetRequiredService<IUnitOfWork<TDbContext>>();

        foreach (var cache in singletonEntityCaches)
        {
            await cache.InitializeAsync(db);
        }
    }
}