namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// The base repository interface.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IBaseRepository : IDisposable
{
}

/// <summary>
/// The base repository interface.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IDisposable" />
public interface IBaseRepository<T> : IBaseRepository where T : class, IAuditableEntity
{
    /// <summary>
    /// Gets the entity by <paramref name="id" /> asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<T> GetEntityAsync(int id);

    /// <summary>
    /// Updates the <paramref name="entity"/> asynchronously.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Updates a list of <paramref name="entities"/> asynchronously.
    /// </summary>
    /// <param name="entities">The entity.</param>
    /// <returns></returns>
    Task UpdateAsync(List<T> entities);

    /// <summary>
    /// Adds a new <paramref name="entity"/> asynchronously.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Deletes the asynchronously.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task DeleteAsync(T entity);
}