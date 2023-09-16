using Carneiro.Core.Entities.Abstractions;
using Carneiro.Core.Repository.Abstractions;

namespace Carneiro.Core.Repository;

/// <summary>
/// The base repository implementation
/// </summary>
/// <seealso cref="IBaseRepository" />
public abstract class BaseRepository : IBaseRepository
{
    private bool _disposed;

    /// <summary>
    /// Gets the unit of work.
    /// </summary>
    /// <value>
    /// The unit of work.
    /// </value>
    protected IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    protected BaseRepository(IUnitOfWork unitOfWork)
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
/// <seealso cref="IBaseRepository" />
public abstract class BaseRepository<T> : BaseRepository, IBaseRepository<T> where T : class, IAuditableEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    protected BaseRepository(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }

    /// <inheritdoc />
    public virtual Task<T> GetEntityAsync(int id) => UnitOfWork.FirstOrDefaultAsync<T>(t => t.Id == id);

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