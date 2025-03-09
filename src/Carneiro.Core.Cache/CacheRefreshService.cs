namespace Carneiro.Core.Cache;

/// <summary>
/// 
/// </summary>
/// <param name="cache"></param>
/// <param name="serviceProvider"></param>
/// <typeparam name="TCache"></typeparam>
/// <typeparam name="TDbContext"></typeparam>
public class CacheRefreshService<TCache, TDbContext>(TCache cache, IServiceProvider serviceProvider) : BackgroundService
    where TCache : ISingletonEntityCache<TDbContext>
    where TDbContext : DbContext
{
    private readonly TCache _cache = cache;

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TimeSpan interval = _cache.GetRefreshPeriod;
        await Task.Delay(interval, stoppingToken); // Delay so we don't immediately refresh

        while (!stoppingToken.IsCancellationRequested)
        {
            await using (AsyncServiceScope scope = serviceProvider.CreateAsyncScope())
            {
                IUnitOfWork<TDbContext> db = scope.ServiceProvider.GetRequiredService<IUnitOfWork<TDbContext>>();
                await _cache.RefreshAsync(db);
            }

            await Task.Delay(interval, stoppingToken);
        }
    }
}