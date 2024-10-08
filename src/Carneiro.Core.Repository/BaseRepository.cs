﻿namespace Carneiro.Core.Repository;

/// <summary>
/// The base repository implementation
/// </summary>
/// <seealso cref="IBaseRepository" />
public abstract class BaseRepository<TDbContext> : IBaseRepository
    where TDbContext : DbContext
{
    private bool _disposed;

    /// <summary>
    /// Gets the unit of work.
    /// </summary>
    protected IUnitOfWork<TDbContext> UnitOfWork { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    protected BaseRepository(IUnitOfWork<TDbContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
    /// unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            UnitOfWork.Dispose();
        }

        _disposed = true;
    }
}

/// <summary>
/// The base repository implementation.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TDbContext">The database context.</typeparam>
/// <seealso cref="IBaseRepository" />
public abstract class BaseRepository<TDbContext, T> : BaseRepository<TDbContext>, IBaseRepository<T>
    where T : class, IEntity
    where TDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    protected BaseRepository(IUnitOfWork<TDbContext> unitOfWork)
        : base(unitOfWork)
    {
    }

    /// <inheritdoc />
    public virtual Task<T> GetEntityAsync(int id) => UnitOfWork.SingleOrDefaultAsync<T>(t => t.Id == id);

    /// <inheritdoc />
    public virtual Task UpdateAsync(T entity)
    {
        UnitOfWork.Update(entity);
        return UnitOfWork.SaveAsync();
    }

    /// <inheritdoc />
    public virtual Task UpdateAsync(List<T> entities)
    {
        UnitOfWork.UpdateRange(entities);
        return UnitOfWork.SaveAsync();
    }

    /// <inheritdoc />
    public virtual async Task AddAsync(T entity)
    {
        await UnitOfWork.AddAsync(entity);
        await UnitOfWork.SaveAsync();
    }

    /// <inheritdoc />
    public virtual Task DeleteAsync(T entity)
    {
        UnitOfWork.Delete(entity);
        return UnitOfWork.SaveAsync();
    }
}