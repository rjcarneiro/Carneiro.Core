﻿namespace Carneiro.Core.Repository;

/// <summary>
/// Working implementation of Entity Framework for <see cref="IUnitOfWork"/>.
/// </summary>
/// <seealso cref="IUnitOfWork" />
public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _context;
    private readonly ILogger<UnitOfWork<TDbContext>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork{TDbContext}" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UnitOfWork(TDbContext dbContext, ILogger<UnitOfWork<TDbContext>> logger)
    {
        _context = dbContext;
        _logger = logger;
    }

    /// <inheritdoc />
    public virtual void Add<T>(T entity) where T : class, IAuditableEntity
    {
        AuditCreate(entity);
        _context.Add(entity);
    }

    /// <inheritdoc />
    public virtual async Task AddAsync<T>(T entity) where T : class, IAuditableEntity => await AddAsync<T>(entity, CancellationToken.None);

    /// <inheritdoc />
    public virtual async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IAuditableEntity
    {
        AuditCreate(entity);
        await _context.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public virtual void AddRange<T>(IEnumerable<T> entities) where T : class, IAuditableEntity
    {
        IEnumerable<T> iAuditableEntities = entities as T[] ?? entities.ToArray();

        foreach (T entity in iAuditableEntities)
            AuditCreate(entity);

        _context.AddRange(iAuditableEntities);
    }

    /// <inheritdoc />
    public virtual Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class, IAuditableEntity => AddRangeAsync<T>(entities, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IAuditableEntity
    {
        IEnumerable<T> iAuditableEntities = entities as T[] ?? entities.ToArray();
        foreach (T entity in iAuditableEntities)
            AuditCreate(entity);

        return _context.AddRangeAsync(iAuditableEntities, cancellationToken);
    }

    /// <inheritdoc />
    public virtual void Update<T>(T entity) where T : class, IAuditableEntity
    {
        AuditUpdate(entity);
        _context.Update(entity);
    }

    /// <inheritdoc />
    public virtual void UpdateRange<T>(IEnumerable<T> entities) where T : class, IAuditableEntity
    {
        IEnumerable<T> iAuditableEntities = entities as T[] ?? entities.ToArray();
        foreach (T entity in iAuditableEntities)
            AuditUpdate(entity);

        _context.UpdateRange(iAuditableEntities);
    }

    /// <inheritdoc />
    public virtual void Delete<T>(T entity) where T : class, IAuditableEntity => Delete(entity, hardDelete: false);

    /// <inheritdoc />
    public virtual void Delete<T>(T entity, bool hardDelete) where T : class, IAuditableEntity
    {
        if (hardDelete)
        {
            _context.Set<T>().Remove(entity);
        }
        else
        {
            AuditDelete(entity);
            _context.Update(entity);
        }
    }

    /// <inheritdoc />
    public virtual void Delete<T>(IEnumerable<T> entities) where T : class, IAuditableEntity => Delete(entities, hardDelete: false);

    /// <inheritdoc />
    public virtual void Delete<T>(IEnumerable<T> entities, bool hardDelete) where T : class, IAuditableEntity
    {
        if (hardDelete)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        else
        {
            IEnumerable<T> baseEntities = entities.ToList();
            foreach (T entity in baseEntities)
            {
                AuditDelete(entity);
            }

            _context.UpdateRange(baseEntities);
        }
    }

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>() where T : class, IAuditableEntity => GetAsync<T>(CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>(CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().ToListAsync(cancellationToken: cancellationToken);

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => GetAsync<T>(expression, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query(expression).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>() where T : class, IAuditableEntity => _context.Set<T>().Where(t => t.IsDeleted == false);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => _context.Set<T>().Where(t => t.IsDeleted == false).Where(expression);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>(long id) where T : class, IAuditableEntity => _context.Set<T>().Where(t => t.IsDeleted == false && t.Id == id);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => _context.Set<T>().Where(t => t.IsDeleted == false && t.Id == id).Where(expression);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>() where T : class, IAuditableEntity => AnyAsync<T>(CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().AnyAsync(cancellationToken: cancellationToken);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id) where T : class, IAuditableEntity => AnyAsync<T>(id, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>(id).AnyAsync(t => t.Id == id, cancellationToken: cancellationToken);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>(id).AnyAsync(expression);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>(id).AnyAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>().AnyAsync(expression);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().AnyAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> FirstAsync<T>(long id) where T : class, IAuditableEntity => Query<T>(id).FirstAsync();

    /// <inheritdoc />
    public virtual Task<T> FirstAsync<T>() where T : class, IAuditableEntity => Query<T>().FirstAsync();

    /// <inheritdoc />
    public virtual Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query(expression).FirstAsync();

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(long id) where T : class, IAuditableEntity => _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>().FirstOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>(id).FirstOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().FirstOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity =>
        Query<T>(id).FirstOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> SingleAsync<T>(long id) where T : class, IAuditableEntity => Query<T>(id).SingleAsync();

    /// <inheritdoc />
    public virtual Task<T> SingleAsync<T>() where T : class, IAuditableEntity => Query<T>().SingleAsync();

    /// <inheritdoc />
    public virtual Task<T> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query(expression).SingleAsync();

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(long id) where T : class, IAuditableEntity => _context.Set<T>().SingleOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>().SingleOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>(id).SingleOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().SingleOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity =>
        Query<T>(id).SingleOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task SaveAsync() => _context.SaveChangesAsync();

    /// <inheritdoc />
    public virtual Task SaveAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

    /// <inheritdoc />
    public virtual Task ExecuteInTransactionScopeAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.Serializable)
    {
        IExecutionStrategy strategy = _context.Database.CreateExecutionStrategy();

        return strategy.ExecuteAsync(async () =>
        {
            await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();

                await SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Transaction failed while creating an execution strategy with transaction scopes");
                throw;
            }
        });
    }

    /// <inheritdoc />
    public virtual void Dispose()
    {
        if (_context is IDisposable contextDisposable)
        {
            contextDisposable.Dispose();
        }
        else if (_context != null)
        {
            _ = _context.DisposeAsync().AsTask();
        }
    }

    /// <inheritdoc />
    public virtual async ValueTask DisposeAsync()
    {
        if (_context != null)
        {
            await _context.DisposeAsync();
        }
    }

    private void AuditCreate<T>(T entity) where T : IAuditableEntity
    {
        entity.IsActive = true;
        entity.IsDeleted = false;
        entity.CreateDate = DateTime.UtcNow;
        entity.UpdateDate = null;
        entity.DeleteDate = null;
    }

    private void AuditUpdate<T>(T entity) where T : IAuditableEntity => entity.UpdateDate = DateTime.UtcNow;

    private void AuditDelete<T>(T entity) where T : IAuditableEntity
    {
        entity.IsActive = false;
        entity.IsDeleted = true;
        entity.DeleteDate = DateTime.UtcNow;
    }
}