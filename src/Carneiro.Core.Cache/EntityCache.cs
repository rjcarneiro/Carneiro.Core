namespace Carneiro.Core.Cache;

/// <inheritdoc />
public abstract class EntityCache<TDbContext, TKey, TEntity>(EntityCacheOptions options) : IEntityCache<TDbContext, TKey, TEntity>
    where TDbContext : DbContext
    where TEntity : class, IAuditableEntity
{
    private readonly object _lock = new object();
    private Dictionary<TKey, TEntity> _cache;

    /// <inheritdoc />
    public virtual TimeSpan GetRefreshPeriod => options.CacheDuration;

    /// <summary>
    /// Flag that checks either this cache was initialized or not.
    /// </summary>
    protected bool Initialized => _cache is not null;

    /// <summary>
    /// Gets the cache items.
    /// </summary>
    /// <exception cref="CacheNotInitializedRzException"></exception>
    protected Dictionary<TKey, TEntity> CacheOrThrow
    {
        get
        {
            lock (_lock)
            {
                return _cache ?? throw new CacheNotInitializedRzException();
            }
        }
    }

    /// <summary>
    /// Gets all cache items.
    /// </summary>
    public virtual ValueTask<TEntity[]> GetAllAsync() => ValueTask.FromResult(CacheOrThrow.Values.ToArray());

    /// <summary>
    /// Gets all cache items.
    /// </summary>
    public virtual TEntity[] GetAll() => CacheOrThrow.Values.ToArray();

    /// <inheritdoc />
    public Dictionary<TKey, TEntity> GetDictionary() => CacheOrThrow;

    /// <summary>
    /// Gets an item by <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    public virtual ValueTask<TEntity> LookupAsync(TKey id) => ValueTask.FromResult(Lookup(id, throwIfNotFound: true));

    /// <summary>
    /// Gets an item by <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    public virtual ValueTask<TEntity> LookupAsync(object id) => LookupAsync((TKey)id);

    /// <summary>
    /// Gets an item by <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="throwIfNotFound"></param>
    /// <exception cref="CacheEntityNotFoundRzException"></exception>
    public virtual TEntity Lookup(TKey id, bool throwIfNotFound)
    {
        Dictionary<TKey, TEntity> cache = CacheOrThrow;
        if (cache.TryGetValue(id, out TEntity result))
        {
            return result;
        }

        if (throwIfNotFound)
        {
            throw new CacheEntityNotFoundRzException(typeof(TEntity).Name, id.ToString());
        }

        return null;
    }

    /// <inheritdoc />
    public async Task RefreshAsync(IUnitOfWork<TDbContext> db) => await FetchData(db);

    /// <inheritdoc />
    public async Task InitializeAsync(IUnitOfWork<TDbContext> db)
    {
        if (Initialized)
        {
            throw new CacheAlreadyInitializedRzException();
        }

        await FetchData(db);
    }

    /// <summary>
    /// Filters the <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="queryable"></param>
    protected virtual IQueryable<TEntity> OnQuerying(IQueryable<TEntity> queryable) => queryable;

    /// <summary>
    /// Processes all entities after querying the database.
    /// </summary>
    /// <param name="entities"></param>
    protected abstract Dictionary<TKey, TEntity> ProcessEntities(TEntity[] entities);

    private async Task<Dictionary<TKey, TEntity>> GetCacheAsync(IUnitOfWork<TDbContext> db)
    {
        TEntity[] allEntities = await OnQuerying(db.Query<TEntity>()).ToArrayAsync();
        return ProcessEntities(allEntities);
    }

    private async Task FetchData(IUnitOfWork<TDbContext> db)
    {
        Dictionary<TKey, TEntity> cache = await GetCacheAsync(db);
        lock (_lock)
        {
            _cache = cache;
        }
    }
}