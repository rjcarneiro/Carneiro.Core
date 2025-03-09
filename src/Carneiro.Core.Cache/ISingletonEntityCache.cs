namespace Carneiro.Core.Cache;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public interface ISingletonEntityCache<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    Task InitializeAsync(IUnitOfWork<TDbContext> db);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    Task RefreshAsync(IUnitOfWork<TDbContext> db);

    /// <summary>
    /// 
    /// </summary>
    TimeSpan GetRefreshPeriod { get; }
}